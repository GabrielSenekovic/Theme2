using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWhenFallenOn : MonoBehaviour
{
    bool broken = false;

    private void Update()
    {
        if(broken)
        {
            Vector3Int pos = UIManager.Instance.tilemap.WorldToCell(transform.position);
            if (UIManager.Instance.tilemap.GetTile(pos + Vector3Int.right) == UIManager.Instance.tilemap.GetTile(pos))
            {
                UIManager.Instance.tilemap.SetTile(pos + Vector3Int.right, null);
            }
            if (UIManager.Instance.tilemap.GetTile(pos + Vector3Int.left) == UIManager.Instance.tilemap.GetTile(pos))
            {
                UIManager.Instance.tilemap.SetTile(pos + Vector3Int.left, null);
            }
            UIManager.Instance.tilemap.SetTile(pos, null);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>())
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < -10)
            {
                broken = true;
            }
        }
    }
}
