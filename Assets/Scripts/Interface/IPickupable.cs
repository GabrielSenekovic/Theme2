using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickupable 
{
    void OnPickUp();
    void OnLetGo();

    bool UseItem(int dir);

    GameObject GameObject();
}
