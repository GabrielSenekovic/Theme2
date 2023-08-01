using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Diamond : MonoBehaviour
{
    public int value;
    bool taken = false;
    public AudioClip clip;

    private void Update()
    {
        if(taken)
        {
            Tilemap tileMap = UIManager.Instance.GetTileMap(TilemapFunction.OBJECT);
            Vector3Int pos = tileMap.WorldToCell(transform.position);
            UIManager.ChangeCoins(value);
            tileMap.SetTile(pos, null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            taken = true;
            AudioManager.PlaySound(clip);
        }
    }
}
