using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DeleteSelfFromTilemap : MonoBehaviour
{
    bool initialized;
    private void Update()
    {
        if (!initialized)
        {
            Tilemap map = UIManager.Instance.GetTileMap(TilemapFunction.OBJECT);
            Vector3Int pos = map.WorldToCell(transform.position);
            map.SetColor(pos, Color.clear);
            initialized = true;
        }
    }
}
