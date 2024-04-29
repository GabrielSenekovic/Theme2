using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilemapManager
{
    [SerializeField] List<TileMapFunctionData> tileMaps = new List<TileMapFunctionData>();

    static TilemapManager instance;
    public static TilemapManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new TilemapManager();
            }
            return instance;
        }
    }
    public void SetTilemaps(List<TileMapFunctionData> tileMaps)
    {
        this.tileMaps.Clear();
        this.tileMaps = tileMaps;
    }
    public Tilemap GetTileMap(TilemapFunction func) => tileMaps.FirstOrDefault(t => t.func == func)?.map;
}
