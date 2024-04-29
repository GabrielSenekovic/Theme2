using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Portal : MonoBehaviour
{
    public GameObject portalObject;
    public Portal portalScript;
    public bool dormant = false;
    public PortalHub hub;
    bool initialized = false;
    // Start is called before the first frame update
    void Start()
    {
        hub = UIManager.Instance.portalHub;
    }

    int ConvertTileNumberToInt()
    {
        Tilemap contentMap = TilemapManager.Instance.GetTileMap(TilemapFunction.CONTENT);
        Vector3Int pos = contentMap.WorldToCell(transform.position);
        RuleTile content = contentMap.GetTile(pos) as RuleTile;
        int.TryParse(content.name, out int number);
        contentMap.SetTile(pos, null);
        return number;
    }

    bool FindOtherPortal()
    {
        int id = ConvertTileNumberToInt();

        if(!hub.ConnectPortal(id, gameObject, this))
        {
            hub.AddPortal(id, gameObject, this);
            return false;
        }
        return true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (hub != null)
        {
            if (!initialized)
            {
                FindOtherPortal();
                initialized = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!dormant && collision.gameObject.CompareTag("Player"))
        {
            portalScript.dormant = true;
            Vector2 portPos = portalObject.transform.position;
            collision.transform.position = portPos;
            Vector3 camPos = Camera.main.transform.position;
            Camera.main.transform.position = new Vector3(portPos.x, portPos.y , camPos.z);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dormant = false;
        }
    }
}
