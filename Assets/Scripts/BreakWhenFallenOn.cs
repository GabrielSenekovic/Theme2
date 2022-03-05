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
            Vector3Int pos = UIManager.Instance.tileMap.WorldToCell(transform.position);
            if (UIManager.Instance.tileMap.GetTile(pos + Vector3Int.right) == UIManager.Instance.tileMap.GetTile(pos))
            {
                UIManager.Instance.tileMap.SetTile(pos + Vector3Int.right, null);
            }
            if (UIManager.Instance.tileMap.GetTile(pos + Vector3Int.left) == UIManager.Instance.tileMap.GetTile(pos))
            {
                UIManager.Instance.tileMap.SetTile(pos + Vector3Int.left, null);
            }
            UIManager.Instance.tileMap.SetTile(pos, null);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<Rigidbody2D>())
        {
            if(collision.gameObject.GetComponent<Rigidbody2D>().velocity.y < -8)
            {
                broken = true;
            }
        }
    }
}
