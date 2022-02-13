using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    bool bounce = false;
    public float bounceAmount;
    public Rigidbody2D rb;
    public AudioClip jumping;

    public void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
        GetComponent<AudioSource>().clip = jumping;
    }

    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player" && other.gameObject.transform.position.y > transform.position.y + 0.75f) 
        {
            
            bounce = true;
            Debug.Log(bounce);
            GetComponent<AudioSource>().Play();


        }
    }
 
    void Update () 
    {
        if(bounce) 
        {
            rb.velocity = new Vector2(rb.velocity.x,0);
            rb.AddForce(new Vector2(0,bounceAmount),ForceMode2D.Impulse);
            bounce = false;
        }
    }
}

