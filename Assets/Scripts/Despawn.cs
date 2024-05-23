using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    // Start is called before the first frame update
        private Renderer rend;
        private int timer = 0;
        public bool BreakOnImpact;
        private int timer_max = 30 * 60;
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
            if(timer > timer_max)
            Destroy(gameObject);
        }
        else { timer = 0; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tilemap") && BreakOnImpact)
        {
            Destroy(gameObject);
        }
    }
}
