using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    // Start is called before the first frame update
        private Renderer rend;
        private int timer;
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!rend.isVisible)
        {
            timer++;
            if(timer > 30)
            Destroy(gameObject);
        }
    }
}
