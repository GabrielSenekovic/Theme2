using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GamblingBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject contents;

    public TileBase content;
    bool initialized;
    bool spawn = false;

    private void Update()
    {
        if (!initialized)
        {
            Vector3Int pos = UIManager.Instance.contentmap.WorldToCell(transform.position);
            content = UIManager.Instance.contentmap.GetTile(pos);
            UIManager.Instance.contentmap.SetTile(pos, null);
            initialized = true;
        }
        if(spawn)
        {
            Vector3Int pos = UIManager.Instance.tilemap.WorldToCell(transform.position);
            UIManager.Instance.tilemap.SetTile(pos, content);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            spawn = true;
        }
    }

}