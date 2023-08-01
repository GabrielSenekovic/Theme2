using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            UIManager.ChangeLives(-1);

            PlayerMovement playerMovement = null;

            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            }
            else if(collision.transform.parent.gameObject.GetComponent<PlayerMovement>())
            {
                playerMovement = collision.transform.parent.gameObject.GetComponent<PlayerMovement>();
            }

            playerMovement.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(GetComponent<Collider2D>().isTrigger)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                UIManager.ChangeLives(-1);

                PlayerMovement playerMovement = null;

                if (other.gameObject.GetComponent<PlayerMovement>())
                {
                    playerMovement = other.gameObject.GetComponent<PlayerMovement>();
                }
                else if (other.transform.parent.gameObject.GetComponent<PlayerMovement>())
                {
                    playerMovement = other.transform.parent.gameObject.GetComponent<PlayerMovement>();
                }

                playerMovement.Die();
            }
        }
    }
}