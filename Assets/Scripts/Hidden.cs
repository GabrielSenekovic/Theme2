using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Vector3Int pos = UIManager.Instance.contentMap.WorldToCell(transform.position);
            RuleTile content = UIManager.Instance.contentMap.GetTile(pos) as RuleTile;
            UIManager.Instance.contentMap.SetTile(pos, null);
            if (content != null)
            { contents = content.m_DefaultGameObject; }
            //RuleTile tile = UIManager.Instance.tileMap.GetTile(pos) as RuleTile;
            UIManager.Instance.tileMap.SetColor(pos, Color.clear);
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
