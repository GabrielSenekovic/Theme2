using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cannon : MonoBehaviour
{

    public enum dir {LEFT, RIGHT, BOTH, NONE};

    public dir ShootDir;

    private Vector3 startpos;

    public float shootCoolDown = 60;
    private float timer = 0f;

    public GameObject projectile;

    public float shootSpeed = 5f;

    private Renderer rend;

    private bool hasRB = false;

    private bool initialized;

    [SerializeField] List<Sprite> sprites;



    // Start is called before the first frame update
    void Start()
    {
        timer = shootCoolDown - 30;


        Vector3Int pos = new Vector3Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(transform.localPosition.y), (int)transform.localPosition.z) * 2;
        
        GetModifierValue(pos, out ModifierTile.ModifierValue val);
        ShootDir = dir.NONE;
        if (val.HasFlag(ModifierTile.ModifierValue.Down))
        {
            ShootDir = dir.BOTH;
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
        }
        else if (val.HasFlag(ModifierTile.ModifierValue.Left))
        {
            ShootDir = dir.LEFT;
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
        }
        else if (val.HasFlag(ModifierTile.ModifierValue.Right))
        {
            ShootDir = dir.RIGHT;
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[2];
        }

        switch (ShootDir)
        {
            case dir.LEFT: startpos = new Vector3(transform.position.x -1, transform.position.y, transform.position.z); break; 
            case dir.RIGHT: startpos = new Vector3(transform.position.x +1, transform.position.y, transform.position.z); break; 
            case dir.BOTH: startpos = new Vector3(transform.position.x, transform.position.y, transform.position.z); break; 
        }
    }

    bool GetModifierValue(Vector3Int pos, out ModifierTile.ModifierValue val)
    {
        ModifierTile tile = TilemapManager.Instance.GetTileMap(TilemapFunction.MODIFIER).GetTile(pos + Vector3Int.down) as ModifierTile;
        TilemapManager.Instance.GetTileMap(TilemapFunction.MODIFIER).SetColor(pos + Vector3Int.down, Color.clear);
        if (tile)
        {
            val = tile.value;
            return true;
        }
        val = 0;
        return false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!initialized)
        {
            Tilemap contentMap = TilemapManager.Instance.GetTileMap(TilemapFunction.CONTENT);
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

        if(timer >= shootCoolDown)
        {
            timer = 0;
            if(ShootDir == dir.BOTH)
            {
                ShootBothWays();
            }
            else if(ShootDir == dir.LEFT || ShootDir == dir.RIGHT)
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
