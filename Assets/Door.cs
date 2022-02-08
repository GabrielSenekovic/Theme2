using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Key;
    public Transform DestinatorDoor;

    public GameObject Player;
    public bool locked;

    // Start is called before the first frame update

    public void Unlock(GameObject key)
    {
        if(key == Key)
        {
            locked = false;
            Destroy(key);
        }
    }

    public void Open() 
    {
        if(!locked)
        Player.transform.position = DestinatorDoor.transform.position;
    }
}
