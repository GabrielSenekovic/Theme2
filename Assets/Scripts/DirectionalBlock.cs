using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DirectionalBlock : MonoBehaviour
{
    public Vector2Int direction;
    public float speed;
    public bool activated;
    bool initialized = false;

    [SerializeField] List<Sprite> sprites;

    private void Start()
    {
        Vector3Int pos = new Vector3Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(transform.localPosition.y), (int)transform.localPosition.z) * 2;
        GetModifierValue(pos, out ModifierTile.ModifierValue val);
        direction = Vector2Int.zero;
        if(val.HasFlag(ModifierTile.ModifierValue.Up))
        {
            direction.y = 1;
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
        }
        else if(val.HasFlag(ModifierTile.ModifierValue.Down))
        {
            direction.y = -1;
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
        }
        else if(val.HasFlag(ModifierTile.ModifierValue.Left))
        {
            direction.x = -1;
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[2];
        }
        else if(val.HasFlag(ModifierTile.ModifierValue.Right))
        {
            direction.x = 1;
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[3];
        }
        else
        {
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[4];
        }
    }

    bool GetModifierValue(Vector3Int pos, out ModifierTile.ModifierValue val)
    {
        ModifierTile tile = TilemapManager.Instance.GetTileMap(TilemapFunction.MODIFIER).GetTile(pos + Vector3Int.down) as ModifierTile;
        TilemapManager.Instance.GetTileMap(TilemapFunction.MODIFIER).SetColor(pos + Vector3Int.down, Color.clear);
        if (tile)
        {
            val = tile.value;
            return true;
        }
        val = 0;
        return false;
    }

    private void Update() 
    {
        if(!initialized)
        {
            Tilemap contentMap = TilemapManager.Instance.GetTileMap(TilemapFunction.CONTENT);
            Vector3Int pos = contentMap.WorldToCell(transform.position);
            contentMap.SetColor(pos, Color.clear);
            initialized = true;
        }
        if(speed > 0)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position + (Vector3)(Vector2)direction * 0.5f, 1f/32f);
            for(int i = 0; i < hits.Length; i++)
            {
                if(hits[i].gameObject.CompareTag("Tilemap") && hits[i].gameObject != gameObject)
                {
                    speed = 0;
                }
            }
        }
    }
    public void FixedUpdate()
    {
        if (!activated) { return; }
        transform.position = new Vector2(transform.position.x + direction.x * speed, transform.position.y + direction.y * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!activated && ((collision.gameObject.CompareTag("Player")
            && collision.gameObject.transform.position.y > transform.position.y
            && collision.gameObject.GetComponent<PlayerMovement>().bGrounded)
            || collision.gameObject.GetComponent<Cannon>())
            )
        {
            activated = true;
            Tilemap tileMap = TilemapManager.Instance.GetTileMap(TilemapFunction.OBJECT);
            Vector3Int pos = tileMap.WorldToCell(transform.position);
            tileMap.SetColor(pos, Color.clear);
        }
        if(collision.gameObject.GetComponent<KillPlayer>())
        {
            Vector2 dirToCollision = Vector2Int.RoundToInt ((Vector2)((collision.transform.position - transform.position).normalized));
            if(dirToCollision == direction)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnDrawGizmos() 
    {
        //Gizmos.color = Color.cyan;
        //Gizmos.DrawSphere(transform.position + (Vector3)(Vector2)direction * 0.5f, 1f/32f);
    }
}
