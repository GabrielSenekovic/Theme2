using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    SpriteRenderer myRenderer;
    [SerializeField]Sprite activatedSprite;

    private void Awake()
    {
        myRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(UIManager.Instance.checkPos != transform.position)
            {
                UIManager.Instance.checkPos = transform.position;
                myRenderer.sprite = activatedSprite;
            }
        }
    }

}
