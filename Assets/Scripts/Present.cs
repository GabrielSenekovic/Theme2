using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Present : MonoBehaviour
{
    public SpriteRenderer number;
    public Sprite[] numbers = new Sprite[4];
    int counter;
    int counter_limit = 60;
    int index = 0;
    public bool count;
    public TileBase content;
    bool initialized;

    private void Update()
    {
        if(!initialized)
        {
            Tilemap map = TilemapManager.Instance.GetTileMap(TilemapFunction.CONTENT);
            Vector3Int pos = map.WorldToCell(transform.position);
            content = map.GetTile(pos);
            map.SetTile(pos, null);
            initialized = true;
        }
    }

    private void FixedUpdate()
    {
        if(count && index < numbers.Length)
        {
            counter++;
            if(counter >= counter_limit)
            {
                counter = 0;
                index++;
                if(index < numbers.Length)
                {
                    number.sprite = numbers[index];
                }
                else if(index == numbers.Length)
                {
                    count = false;
                    Tilemap contentMap = TilemapManager.Instance.GetTileMap(TilemapFunction.CONTENT);
                    Tilemap tileMap = TilemapManager.Instance.GetTileMap(TilemapFunction.OBJECT);
                    Vector3Int pos = contentMap.WorldToCell(transform.position);
                    tileMap.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
                    tileMap.SetTile(pos, content);
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!count && collision.gameObject.CompareTag("Player")
            && collision.gameObject.transform.position.y > transform.position.y
            && collision.gameObject.GetComponent<PlayerMovement>().bGrounded
            )
        {
            count = true;
        }
    }
}
