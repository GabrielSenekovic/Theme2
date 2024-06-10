using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Accelerator : MonoBehaviour
{
    [SerializeField] bool clockWise;
    [SerializeField] float speed;
    [SerializeField] int maxCounter = 1;
    [SerializeField] float rotationSpeed;

    bool isCoolingDown;
    [SerializeField] int counter = 0;
    // Start is called before the first frame update

    Vector2 directionOfJettison;

    SpriteRenderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        Vector3Int pos = new Vector3Int(Mathf.FloorToInt(transform.localPosition.x), Mathf.FloorToInt(transform.localPosition.y), (int)transform.localPosition.z) * 2;
        GetModifierValue(pos, out ModifierTile.ModifierValue val);
       
         if (val.HasFlag(ModifierTile.ModifierValue.Right))
         {
            clockWise = false;
            transform.localScale = new Vector3(-1,1,1);
         }
        else if (val.HasFlag(ModifierTile.ModifierValue.Left))
        {
            clockWise = true;
            transform.localScale = new Vector3(1, 1, 1);
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
    public void FixedUpdate()
    {
        if(isCoolingDown)
        {
            counter++;
            if(counter >= maxCounter) { isCoolingDown = false; counter = 0; }
        }
        int direction = clockWise ? -1 : 1;
        myRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, myRenderer.transform.rotation.eulerAngles.z + rotationSpeed * direction));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Rigidbody2D rb))
        {
            if (isCoolingDown) { return; }
            Vector2 CollisionPoint = collision.collider.ClosestPoint(transform.position);
            Vector2 toCollision = (Vector2)transform.position - CollisionPoint;
            float angle = clockWise ? 90 : -90;
            Vector2 dir = Quaternion.AngleAxis(angle, Vector3.forward) * toCollision;
            rb.velocity += dir.normalized * speed;
            isCoolingDown = true;

            directionOfJettison = dir;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + directionOfJettison * 10);
    }
}
