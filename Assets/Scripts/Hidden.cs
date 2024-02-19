using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hidden : MonoBehaviour
{
    //public Rigidbody2D rb;
    public GameObject contents;
    public bool showHitBox = false;
    private bool initialized = false;

    private bool hit = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = showHitBox;
    }

    private void FixedUpdate()
    {
        if (!initialized)
        {
            Tilemap contentMap = TilemapManager.Instance.GetTileMap(TilemapFunction.CONTENT);
            Vector3Int pos = contentMap.WorldToCell(transform.position);
            RuleTile content = contentMap.GetTile(pos) as RuleTile;
            contentMap.SetTile(pos, null);

            if (content != null)
            { contents = content.m_DefaultGameObject; }


            TilemapManager.Instance.GetTileMap(TilemapFunction.OBJECT).SetColor(pos, Color.clear);
            initialized = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "Player" && other.gameObject.transform.position.y < transform.position.y - 0.5f && !hit) 
        {
            GetComponent<SpriteRenderer>().enabled = true;
            if (contents.gameObject != null)
            {
                GameObject obj = Instantiate<GameObject>(contents);
                obj.transform.position = transform.position;
                GetComponent<SpriteRenderer>().enabled = obj.GetComponent<Rigidbody2D>();
            }
            hit = true;
            //Destroy(gameObject);
        }
    }

}
