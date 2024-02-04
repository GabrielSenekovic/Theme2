using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilemapManager : MonoBehaviour
{
    [SerializeField] List<TileMapFunctionData> tileMaps = new List<TileMapFunctionData>();

    static TilemapManager instance;
    public static TilemapManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (!instance)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetTilemaps(List<TileMapFunctionData> tileMaps)
    {
        this.tileMaps.Clear();
        this.tileMaps = tileMaps;
    }
    public Tilemap GetTileMap(TilemapFunction func) => tileMaps.FirstOrDefault(t => t.func == func)?.map;
}
