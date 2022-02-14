using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    public float jumpSpeed = 2;
    private Rigidbody2D rb;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity += new Vector2(0,jumpSpeed);
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update() 
    {
        float s = Mathf.Sign(rb.velocity.x);

        transform.localScale = new Vector3(-s,1,1);
    }
}
