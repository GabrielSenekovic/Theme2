using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GamblingBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject contents;

    public TileBase content;
    bool initialized;
    bool spawn = false;

    private void Update()
    {
        if (!initialized)
        {
            Tilemap contentMap = UIManager.Instance.GetTileMap(TilemapFunction.CONTENT);
            Vector3Int pos = contentMap.WorldToCell(transform.position);
            content = contentMap.GetTile(pos);
            contentMap.SetTile(pos, null);
            initialized = true;
        }
        if(spawn)
        {
            Tilemap tileMap = UIManager.Instance.GetTileMap(TilemapFunction.OBJECT);
            Vector3Int pos = tileMap.WorldToCell(transform.position);
            tileMap.SetTile(pos, content);
            tileMap.SetColor(pos, Color.clear);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            spawn = true;
        }
    }

}
