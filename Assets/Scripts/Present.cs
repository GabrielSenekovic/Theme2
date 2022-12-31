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
            Vector3Int pos = UIManager.Instance.contentMap.WorldToCell(transform.position);
            content = UIManager.Instance.contentMap.GetTile(pos);
            UIManager.Instance.contentMap.SetTile(pos, null);
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
                    Vector3Int pos = UIManager.Instance.contentMap.WorldToCell(transform.position);
                    UIManager.Instance.tileMap.SetTile(new Vector3Int(pos.x, pos.y + 1, pos.z), null);
                    UIManager.Instance.tileMap.SetTile(pos, content);
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
