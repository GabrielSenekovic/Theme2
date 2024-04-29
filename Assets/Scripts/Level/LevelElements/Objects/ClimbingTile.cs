using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.W) )
    //    {
    //        GameObject playerGO = collision.gameObject;
    //        Rigidbody2D rigid = playerGO.GetComponentInParent<Rigidbody2D>();
    //        if (!playerGO.GetComponent<PlayerMovement>().climbing)
    //        {
    //            playerGO.GetComponentInParent<PlayerMovement>().climbing = true;
    //            rigid.velocity = new Vector2(0, 0);
    //            rigid.gravityScale = 0;
    //        }
    //    }
    //}


    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.GetComponent<PlayerMovement>().climbing = false;
    //        collision.gameObject.GetComponentInParent<Rigidbody2D>().gravityScale = 2;
    //    }
    //}
}
