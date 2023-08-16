using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayBlock : MonoBehaviour
{
    enum MODE {FADE = 0, REGEN = 1, STAY = 2 }
    MODE mode = MODE.STAY;
    private int counter = 0;
    public int counter_limit;
    public BoxCollider2D hitbox;
    public SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        counter = counter_limit;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (mode == MODE.FADE)
        {
            counter--;
            if (counter <= 0)
            {
                hitbox.enabled = false;
                mode = MODE.REGEN;
            }
        }
        if(mode == MODE.REGEN)
        {
            counter++;
            if(counter >= counter_limit)
            {
                hitbox.enabled = true;
                mode = MODE.STAY;
            }
        }
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, counter / (float)counter_limit);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!(mode == MODE.FADE) && collision.gameObject.CompareTag("Player")
            && collision.gameObject.transform.position.y > transform.position.y
            && collision.gameObject.GetComponent<PlayerMovement>().bGrounded
            )
        {
            mode = MODE.FADE;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ( mode == MODE.FADE && collision.gameObject.CompareTag("Player") 
            && collision.gameObject.transform.position.y > transform.position.y
            && collision.gameObject.GetComponent<PlayerMovement>().bGrounded
            )
        {
            mode = MODE.REGEN;
        }
    }

}
