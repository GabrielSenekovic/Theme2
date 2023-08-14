using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TilemapFunction
{
    NONE = 0,
    OBJECT = 1,
    CONTENT = 2,
    TILEMAP = 3,
    MODIFIER = 4
}
[System.Serializable]
public class TileMapFunctionData
{
    public Tilemap map;
    public TilemapFunction func;
}
public class GridManager : MonoBehaviour
{
    public List<TileMapFunctionData> tileMaps = new List<TileMapFunctionData>();

    private void Start()
    {
        UIManager.Instance.SetTilemaps(tileMaps);
    }
}
