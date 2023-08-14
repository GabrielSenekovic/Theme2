using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OverworldLevel : MonoBehaviour
{
    bool initialized;
    public NumberTile tile;

    private void Start()
    {
        Vector3Int pos = new Vector3Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(transform.localPosition.y), (int)transform.localPosition.z) * 2;
        OverworldManager.Instance.IDMap.SetColor(pos, Color.clear);
        OverworldManager.Instance.IDMap.SetColor(pos + Vector3Int.right, Color.clear);
    }
    public async void LoadLevel()
    {
        if (initialized) { return; }
        Vector3Int pos = new Vector3Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(transform.localPosition.y), (int)transform.localPosition.z) * 2;
        GetNumberTile(pos, out int val_1);
        GetNumberTile(pos + Vector3Int.right, out int val_2);
        int id = val_1 * 16 + val_2;
        string key = "Level " + id;
        SceneLoader.Instance.Load(key);
        initialized = true;
    }
    bool GetNumberTile(Vector3Int pos, out int val)
    {
        tile = OverworldManager.Instance.IDMap.GetTile(pos) as NumberTile;
        if(tile)
        {
            val = tile.value;
            return true;
        }
        val = 0;
        return false;
    }
}
