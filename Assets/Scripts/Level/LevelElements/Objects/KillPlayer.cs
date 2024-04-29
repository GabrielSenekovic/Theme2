using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public bool destroyOnCollision = false;
    public bool isOn;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isOn) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            UIManager.Instance.ChangeLives(-1);

            Player playerMovement = null;

            if (collision.gameObject.GetComponent<Player>())
            {
                playerMovement = collision.gameObject.GetComponent<Player>();
            }
            else if(collision.transform.parent.gameObject.GetComponent<Player>())
            {
                playerMovement = collision.transform.parent.gameObject.GetComponent<Player>();
            }

            playerMovement.Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!isOn) return;
        if (GetComponent<Collider2D>().isTrigger)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                UIManager.Instance.ChangeLives(-1);

                Player playerMovement = null;

                if (other.gameObject.GetComponent<Player>())
                {
                    playerMovement = other.gameObject.GetComponent<Player>();
                }
                else if (other.transform.parent.gameObject.GetComponent<Player>())
                {
                    playerMovement = other.transform.parent.gameObject.GetComponent<Player>();
                }

                playerMovement.Die();
            }
        }
    }
}