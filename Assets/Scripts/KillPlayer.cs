using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public bool destroyOnCollision = false;
    public bool isOn;

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isOn) return;
        if(collision.gameObject.CompareTag("Player"))
        {
            UIManager.ChangeLives(-1);
            UIManager.LoadScene();

            if(collision.gameObject.GetComponent<PlayerMovement>())
            {
                if (!collision.gameObject.GetComponent<PlayerMovement>().dead)
                {
                    collision.gameObject.GetComponent<PlayerMovement>().VFX.Play();
                    collision.gameObject.GetComponent<PlayerMovement>().renderer.color = Color.clear;
                    collision.gameObject.GetComponent<PlayerMovement>().dead = true;
                }
            }
            else if(collision.transform.parent.gameObject.GetComponent<PlayerMovement>())
            {
                if (!collision.transform.parent.gameObject.GetComponent<PlayerMovement>().dead)
                {
                    collision.transform.parent.gameObject.GetComponent<PlayerMovement>().VFX.Play();
                    collision.transform.parent.gameObject.GetComponent<PlayerMovement>().renderer.color = Color.clear;
                    collision.transform.parent.gameObject.GetComponent<PlayerMovement>().dead = true;
                }

            }
            if(destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!isOn) return;
        if (GetComponent<Collider2D>().isTrigger)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                UIManager.ChangeLives(-1);
                UIManager.LoadScene();
                
                if(other.gameObject.GetComponent<PlayerMovement>())
                {
                    if (!other.gameObject.GetComponent<PlayerMovement>().dead)
                    {
                        other.gameObject.GetComponent<PlayerMovement>().VFX.Play();
                        other.gameObject.GetComponent<PlayerMovement>().renderer.color = Color.clear;
                        other.gameObject.GetComponent<PlayerMovement>().dead = true;
                    }
                }
                else if(other.transform.parent.gameObject.GetComponent<PlayerMovement>())
                {
                        if (!other.transform.parent.gameObject.GetComponent<PlayerMovement>().dead)
                        {
                            other.transform.parent.gameObject.GetComponent<PlayerMovement>().VFX.Play();
                            other.transform.parent.gameObject.GetComponent<PlayerMovement>().renderer.color = Color.clear;
                            other.transform.parent.gameObject.GetComponent<PlayerMovement>().dead = true;
                        }
                }
                if(destroyOnCollision)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}