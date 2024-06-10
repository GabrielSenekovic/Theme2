using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    public Transform destinatorDoor;

    public GameObject player;
    public bool locked;

    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public bool block;

    // Start is called before the first frame update

    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = locked ? lockedSprite : unlockedSprite;
    }

    public void Unlock(GameObject key)
    {
        locked = false;
        Destroy(key);
        if (!block)
        { 
            GetComponent<SpriteRenderer>().sprite = unlockedSprite; 
        }
        else 
        {
            Tilemap tileMap = TilemapManager.Instance.GetTileMap(TilemapFunction.OBJECT);
            Vector3Int pos = tileMap.WorldToCell(transform.position);
            tileMap.SetTile(pos, null);
        }
    }

    public void Open() 
    {
        if( (!locked && player.transform.position.y  < transform.position.y - 0.2) && !block)
        {
            player.transform.position = destinatorDoor.transform.position;
        }
    }
}
