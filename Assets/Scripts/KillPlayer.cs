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
            UIManager.LoadScene();
            if(collision.gameObject.GetComponent<PlayerMovement>())
            {
                collision.gameObject.GetComponent<PlayerMovement>().VFX.Play();
                collision.gameObject.GetComponent<PlayerMovement>().renderer.color = Color.clear;
            }
            else if(collision.transform.parent.gameObject.GetComponent<PlayerMovement>())
            {
                collision.transform.parent.gameObject.GetComponent<PlayerMovement>().VFX.Play();
                collision.transform.parent.gameObject.GetComponent<PlayerMovement>().renderer.color = Color.clear;
            }
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
                UIManager.LoadScene();
                if(other.gameObject.GetComponent<PlayerMovement>())
                {
                    other.gameObject.GetComponent<PlayerMovement>().VFX.Play();
                    other.gameObject.GetComponent<PlayerMovement>().renderer.color = Color.clear;
                }
                else if(other.transform.parent.gameObject.GetComponent<PlayerMovement>())
                {
                    other.transform.parent.gameObject.GetComponent<PlayerMovement>().VFX.Play();
                    other.transform.parent.gameObject.GetComponent<PlayerMovement>().renderer.color = Color.clear;
                }
                if(destroyOnCollision)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}