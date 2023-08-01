using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cannon : MonoBehaviour
{

    public enum dir {LEFT, RIGHT, BOTH};

    public dir ShootDir;

    private Vector3 startpos;

    public float shootFreq = 60;
    private float timer = 0;

    public GameObject projectile;

    public float shootSpeed = 5f;

    private Renderer rend;

    private bool hasRB = false;

    private bool initialized;


    // Start is called before the first frame update
    void Start()
    {
        switch(ShootDir)
        {
            case dir.LEFT: startpos = new Vector3(transform.position.x -1, transform.position.y, transform.position.z); break; 
            case dir.RIGHT: startpos = new Vector3(transform.position.x +1, transform.position.y, transform.position.z); break; 
            case dir.BOTH: startpos = new Vector3(transform.position.x, transform.position.y, transform.position.z); break; 
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!initialized)
        {
            Tilemap contentMap = UIManager.Instance.GetTileMap(TilemapFunction.CONTENT);
            Vector3Int pos = contentMap.WorldToCell(transform.position);
            RuleTile content = contentMap.GetTile(pos) as RuleTile;
            contentMap.SetTile(pos, null);
            projectile = content.m_DefaultGameObject;
            rend = GetComponent<Renderer>();
            if (projectile.GetComponent<Rigidbody2D>())
            {
                hasRB = true;
            }
            initialized = true;
        }

        if (rend.isVisible) { timer++; }

        if(timer >= shootFreq)
        {
            timer = 0;
            if(ShootDir == dir.BOTH)
            {
                ShootBothWays();
            }
            else
            {
                Shoot();
            }
        }
    }
    void ShootBothWays()
    {
        startpos.x += 1;
        GameObject newProjectile = Instantiate(projectile, startpos, transform.rotation);

        if (hasRB)
        {
            Rigidbody2D rightRB = newProjectile.GetComponent<Rigidbody2D>();
            rightRB.velocity += new Vector2(shootSpeed, 0);
        }

        startpos.x -= 2;
        newProjectile = Instantiate(projectile, startpos, transform.rotation);

        if (hasRB)
        {
            Rigidbody2D leftRB = newProjectile.GetComponent<Rigidbody2D>();
            leftRB.velocity += new Vector2(-shootSpeed, 0);
            startpos.x += 1;
        }
    }
    void Shoot()
    {
        int s = ShootDir == dir.LEFT ? -1 : 1;
        if (Physics2D.OverlapBox((Vector2)transform.position + new Vector2(s, 0), new Vector2(0.5f, 0.5f), 0))
        {
            return;
        }
        GameObject proj = Instantiate(projectile, startpos, transform.rotation);

        if (proj.GetComponent<DirectionalBlock>())
        { proj.GetComponent<DirectionalBlock>().activated = true; }

        if (hasRB)
        {
            Rigidbody2D projRB = proj.GetComponent<Rigidbody2D>();
            projRB.velocity = new Vector2(s * shootSpeed, 0);
        }
    }
}
