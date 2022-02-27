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

    private void Update() 
    {
        if(!initialized)
        {
            Vector3Int pos = UIManager.Instance.contentMap.WorldToCell(transform.position);
            UIManager.Instance.contentMap.SetColor(pos, Color.clear);
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
            Vector3Int pos = UIManager.Instance.tileMap.WorldToCell(transform.position);
            UIManager.Instance.tileMap.SetColor(pos, Color.clear);
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
