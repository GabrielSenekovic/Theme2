using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveFileSelect : MonoBehaviour
{
    [SerializeField] List<SaveFileButton> buttonList;

    string saveFilePath = Application.persistentDataPath + "/SaveFiles";

    private void Awake()
    {
        //load savefiles
        string[] fileEntries = Directory.GetFiles(Application.persistentDataPath);
        string[] subdirectoryEntries = Directory.GetDirectories(Application.persistentDataPath);
        if(!Directory.Exists(saveFilePath))
        {
            Directory.CreateDirectory(saveFilePath);
            subdirectoryEntries = Directory.GetDirectories(Application.persistentDataPath);
        }
    }
    public void SetStartMenu(StartMenu startMenu)
    {
        foreach(var button in buttonList)
        {
            button.Initialize(startMenu);
        }
    }
}
