using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    public float YOffset;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Door")
        {
            Debug.Log("bruh");
            other.gameObject.GetComponent<Door>().Unlock(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            transform.parent = other.transform;
            transform.position = (Vector2)other.transform.position + new Vector2(0,YOffset);
        }
    }
}
