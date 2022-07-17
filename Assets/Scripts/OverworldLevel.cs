using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldLevel : MonoBehaviour
{
    bool initialized;
    int scene;

    private void Update()
    {
        if (!initialized)
        {
            Vector3Int pos = OverworldManager.Instance.IDMap.WorldToCell(transform.position);
            scene = (OverworldManager.Instance.IDMap.GetTile(pos) as NumberTile).value + 2; //Cuz 0 is main menu, and 1 is the overworld
            initialized = true;
        }
    }
    public void LoadLevel()
    {
        SceneManager.LoadScene(scene);
    }
}
