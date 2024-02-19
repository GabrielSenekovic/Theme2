using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakWhenFallenOn : MonoBehaviour
{
    bool broken = false;

    float fallBreakSpeed = -2;

    private void Update()
    {
        if(broken)
        {
            Tilemap tileMap = TilemapManager.Instance.GetTileMap(TilemapFunction.OBJECT);
            Vector3Int pos = tileMap.WorldToCell(transform.position);
            if (tileMap.GetTile(pos + Vector3Int.right) == tileMap.GetTile(pos))
            {
                tileMap.SetTile(pos + Vector3Int.right, null);
            }
            if (tileMap.GetTile(pos + Vector3Int.left) == tileMap.GetTile(pos))
            {
                tileMap.SetTile(pos + Vector3Int.left, null);
            }
            tileMap.SetTile(pos, null);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>())
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < fallBreakSpeed)
            {
                broken = true;
            }
        }
    }
}
