using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    public bool clockWise;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            ContactPoint2D contact = collision.contacts[0];
            Vector2 toCollision = (Vector2)transform.position - contact.point;
            float angle = clockWise ? 90 : -90;
            Vector2 dir = Quaternion.AngleAxis(angle, Vector3.up) * toCollision;
            rb.velocity += dir.normalized * speed;
        }
    }
}
