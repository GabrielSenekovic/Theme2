using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBlock : MonoBehaviour
{
    public Vector2Int direction;
    public float speed;
    bool activated;

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
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }
}
