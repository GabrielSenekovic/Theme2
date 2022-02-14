using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public bool destroyOnCollision = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            UIManager.ChangeLives(-1);
            UIManager.ResetPlayer();
            if(destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(GetComponent<Collider2D>().isTrigger)
        {
             if(other.gameObject.CompareTag("Player"))
        {
            UIManager.ChangeLives(-1);
            UIManager.ResetPlayer();
            if(destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
        }
    }

}