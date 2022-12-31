using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

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

    public bool TileExists(Vector3 pos_in, ref OverworldLevel level)
    {
        Vector3Int pos = trailMap.WorldToCell(pos_in);
        OverworldLevel level_val = null;
        if(trailMap.HasTile(pos))
        {
            Physics2D.OverlapCircleAll(pos_in, 0.2f).FirstOrDefault(c => c.gameObject.TryGetComponent<OverworldLevel>(out level_val));
            level = level_val;
            return true;
        }
        return false;
    }
}
