using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    public GameObject thingToActivate;

    public void Toggle(bool value)
    {
        thingToActivate.SetActive(value);
    }
}
