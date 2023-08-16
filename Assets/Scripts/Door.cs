using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Key;
    public Transform DestinatorDoor;

    public GameObject Player;
    public bool locked;

    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    // Start is called before the first frame update

    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = locked ? lockedSprite : unlockedSprite;
    }

    public void Unlock(GameObject key)
    {
        locked = false;
        Destroy(key);
        GetComponent<SpriteRenderer>().sprite = unlockedSprite;
    }

    public void Open() 
    {
        if(!locked && Player.transform.position.y  < transform.position.y - 0.2)
        Player.transform.position = DestinatorDoor.transform.position;
    }
}
