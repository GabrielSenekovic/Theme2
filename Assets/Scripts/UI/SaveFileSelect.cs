using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileSelect : MonoBehaviour
{
    [SerializeField] GameObject areYouSure;
    [SerializeField] Button yesImSure;
    [SerializeField] List<SaveFileButton> buttonList;

    string saveFilePath;

    private void Awake()
    {
        areYouSure.SetActive(false);
        saveFilePath = Application.persistentDataPath + "/SaveFiles";
    }
    private void Start()
    {
        //load savefiles
        string[] fileEntries = Directory.GetFiles(saveFilePath);
        if (!Directory.Exists(saveFilePath))
        {
            Directory.CreateDirectory(saveFilePath);
        }
        for (int i = 0; i < fileEntries.Length; i++)
        {
            if (Load(fileEntries[i], out SaveFile saveFile))
            {
                buttonList[i].SetFile(saveFile);
                continue;
            }
            break;
        }
    }
    public void PressDelete(int i)
    {
        areYouSure.gameObject.SetActive(true);
        yesImSure.onClick.RemoveAllListeners();
        yesImSure.onClick.AddListener(() => { buttonList[i].Delete(); areYouSure.gameObject.SetActive(false); });
    }
    public bool Load(string value, out SaveFile saveFile)
    {
        saveFile = null;
        if (File.Exists(value))
        {
            Debug.Log("Save file found");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(value, FileMode.Open);
            saveFile = (SaveFile)bf.Deserialize(file);
            file.Close();
            return true;
        }
        return false;
    }
    public void SetStartMenu(StartMenu startMenu)
    {
        foreach(var button in buttonList)
        {
            button.Initialize(startMenu);
        }
    }
}
