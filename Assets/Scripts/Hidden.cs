using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{


    public Rigidbody2D rb;
    public GameObject contents;
 
    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player" && other.gameObject.transform.position.y < transform.position.y - 0.5f) 
        {
             GameObject obj = Instantiate<GameObject>(contents);
            obj.transform.position = transform.position;
            Destroy(gameObject);
        }
    }

}