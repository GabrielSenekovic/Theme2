using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update
    public float YOffset;
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Door")
        {
            Debug.Log("bruh");
            other.gameObject.GetComponent<Door>().Unlock(gameObject);
        }

        if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<CircleCollider2D>())
        {
            Debug.Log("circle");
            if(other.gameObject.GetComponent<PlayerMovement>().bGrounded)
            {
            Debug.Log("key player grounded");
                transform.parent = other.transform;
                transform.position = (Vector2)other.transform.position + new Vector2(0,YOffset);
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
}
