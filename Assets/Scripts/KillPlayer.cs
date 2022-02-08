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
            if(destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }
}