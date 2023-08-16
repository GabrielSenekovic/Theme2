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
            other.gameObject.GetComponent<Door>().Unlock(gameObject);
        }

        if(other.gameObject.tag == "Player" && other.gameObject.GetComponent<CircleCollider2D>())
        {
            if(other.gameObject.GetComponent<PlayerMovement>().bGrounded)
            {
                transform.parent = other.transform;
                transform.position = (Vector2)other.transform.position + new Vector2(0,YOffset);
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
    }
}
