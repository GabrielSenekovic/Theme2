using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public Activate target;
    public Sprite activatedSprite;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        if(GetComponentInChildren<SpriteRenderer>())
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            target.Toggle(true);
            spriteRenderer.sprite = activatedSprite;
        }
    }
   /* private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target.Toggle(false);
        }
    }*/
}
