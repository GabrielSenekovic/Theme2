using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DirectionalBlock : MonoBehaviour
{
    public Vector2Int direction;
    public float speed;
    bool activated;
    bool initialized;
    private void Update()
    {
        if(!initialized)
        {
            Vector3Int pos = UIManager.Instance.contentmap.WorldToCell(transform.position);
            UIManager.Instance.contentmap.SetColor(pos, Color.clear);
            initialized = true;
        }
    }

    public void FixedUpdate()
    {
        if (!activated) { return; }
        transform.position = new Vector3(transform.position.x + speed * direction.x, transform.position.y + speed * direction.y, transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(!activated && collision.gameObject.CompareTag("Player")
            && collision.gameObject.transform.position.y > transform.position.y
            && collision.gameObject.GetComponent<PlayerMovement>().bGrounded
            )
        {
            activated = true;
        }
            if(collision.gameObject.GetComponent<KillPlayer>())
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
