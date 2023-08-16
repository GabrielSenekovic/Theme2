using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHub : MonoBehaviour
{
    public List<portalData> portals = new List<portalData>();
    // Start is called before the first frame update

    public void AddPortal(int id, GameObject obj, Portal port)
    {
        portalData item = new portalData(id, obj, port);
        portals.Add(item);
    }

    public bool ConnectPortal(int id, GameObject obj, Portal port) // id of connectING portal, obj that is connectING portal, portalscript of connectING portal
    {
        for(int i = 0; i < portals.Count; i++ )
        {
            if (portals[i].ID == id)
            {
                    port.portalObject = portals[i].portal; // connect you to list portal gameOjbect
                    port.portalScript = portals[i].portalScript; // connect you to list portalScript

                    portals[i].portalScript.portalObject = obj; // connect list object to you
                    portals[i].portalScript.portalScript = port; // connect list script to you;
                    return true; // connection made
            }
        }

        return false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct portalData
{
    public portalData(int id, GameObject obj, Portal portalscript)
    {
        this.ID = id;
        this.Portal = obj;
        this.PortalScript = portalscript;
    }
    public int ID;
    private GameObject Portal;
    private Portal PortalScript;

    public Portal portalScript
    {
        get { return PortalScript; }
        set { PortalScript = value; }
    }
    public GameObject portal
    {
        get { return Portal; }
        set { Portal = value; }
    }
}