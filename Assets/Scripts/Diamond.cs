using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int value;
    bool taken = false;
    public AudioClip clip;

    private void Update()
    {
        if(taken)
        {
            Vector3Int pos = UIManager.Instance.tilemap.WorldToCell(transform.position);
            UIManager.ChangeCoins(value);
            UIManager.Instance.tilemap.SetTile(pos, null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            taken = true;
            AudioManager.PlaySound(clip);
        }
    }
}
