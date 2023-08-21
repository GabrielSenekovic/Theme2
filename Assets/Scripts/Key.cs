using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Key : MonoBehaviour, IPickupable
{
    public GameObject GameObject()
    {
        return gameObject;
    }
    public void OnLetGo()
    {
    }

    public void OnPickUp()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public bool UseItem(int dir)
    {
        Door door = null;
        if(Physics2D.OverlapCircleAll(transform.position, 1).FirstOrDefault(c => c.TryGetComponent(out door)))
        {
            door.Unlock(gameObject);
            return true;
        }
        return false;
    }
}
