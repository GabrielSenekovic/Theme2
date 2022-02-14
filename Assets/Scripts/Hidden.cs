using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidden : MonoBehaviour
{


    public Rigidbody2D rb;
    public GameObject contents;
    public bool showHitBox = false;

    private bool hit = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = showHitBox;
    }
 
    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player" && other.gameObject.transform.position.y < transform.position.y - 0.5f && !hit) 
        {
            GameObject obj = Instantiate<GameObject>(contents);
            obj.transform.position = transform.position;
            GetComponent<SpriteRenderer>().enabled = showHitBox;
            hit = true;
            //Destroy(gameObject);
        }
    }

}
