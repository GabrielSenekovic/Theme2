using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OverworldManager : MonoBehaviour
{
    static OverworldManager instance;
    public static OverworldManager Instance
    {
        get
        {
            return instance;
        }
    }
    public Tilemap IDMap;
    public Tilemap levelMap;
    public Tilemap trailMap;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool TileExists(Vector3 pos_in)
    {
        Vector3Int pos = trailMap.WorldToCell(pos_in);
        if(trailMap.HasTile(pos))
        {
            return true;
        }
        return false;
    }
}
