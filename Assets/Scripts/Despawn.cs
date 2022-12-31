using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    // Start is called before the first frame update
        private Renderer rend;
        private int timer;
        public bool BreakOnImpact;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tilemap") && BreakOnImpact)
        {
            Destroy(gameObject);
        }
    }
}
