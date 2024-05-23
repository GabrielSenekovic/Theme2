using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SaveFileSelect;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class SaveFileButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button deleteButton;

    SaveFile saveFile;
    StartMenu startMenu;

    string saveFilePath;

    private void Awake()
    {
        inputField.gameObject.SetActive(false);
        saveFilePath = Application.persistentDataPath + "/SaveFiles";
        saveFile = null;
        deleteButton.gameObject.SetActive(false);
    }
    public void Initialize(StartMenu startMenu)
    {
        this.startMenu = startMenu;
    }
    public void SetFile(SaveFile file)
    {
        saveFile = file;
        saveFile.strings.TryGetValue("playerName", out string playerName);
        this.playerName.text = playerName;
        deleteButton.gameObject.SetActive(true);
    }
    public void OnClick()
    {
        if(saveFile == null)
        {
            playerName.gameObject.SetActive(false);
            inputField.gameObject.SetActive(true);
            inputField.Select();
            //Write name, create new save
        }
        else
        {
            startMenu.LoadSave(saveFile);
        }
    }
    public void CreateSaveFile()
    {
        playerName.text = inputField.text;
        playerName.gameObject.SetActive(true);
        inputField.gameObject.SetActive(false);

        saveFile = new SaveFile();

        saveFile.strings.Add("playerName", playerName.text);
        deleteButton.gameObject.SetActive(true);
        Save();
    }
    public void Save()
    {
        Debug.Log("Save file saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath + "/" + playerName.text + ".save");
        bf.Serialize(file, saveFile);
        file.Close();
    }
    public void Delete()
    {
        File.Delete(saveFilePath + "/" + playerName.text + ".save");
        deleteButton.gameObject.SetActive(false);
        playerName.text = "New Game";
    }
}
