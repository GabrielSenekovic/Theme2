using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static SaveFileSelect;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveFileButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;

    SaveFile saveFile;
    StartMenu startMenu;

    private void Awake()
    {
        saveFile = new SaveFile();
    }
    public void Initialize(StartMenu startMenu)
    {
        this.startMenu = startMenu;
    }
    public void SetFile(SaveFile file)
    {
        saveFile = file;
    }
    public void OnClick()
    {
        startMenu.LoadSave(saveFile);
    }
    public bool Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveFile.save"))
        {
            Debug.Log("Save file found");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveFile.save", FileMode.Open);
            saveFile = (SaveFile)bf.Deserialize(file);
            file.Close();
            return true;
        }
        return false;
    }
    public bool Save()
    {
        Debug.Log("Save file saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + playerName.text + ".save");
        bf.Serialize(file, saveFile);
        file.Close();
        return true;
    }
}
