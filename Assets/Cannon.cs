using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    public enum dir {LEFT, RIGHT, BOTH};

    public dir ShootDir;

    private Vector3 startpos;

    public float shootFreq = 60;
    private float timer = 0;

    public GameObject projectile;

    public float shootSpeed = 5f;

    private Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        switch(ShootDir)
        {
            case dir.LEFT: startpos = new Vector3(transform.position.x -1, transform.position.y, transform.position.z); break; 
            case dir.RIGHT: startpos = new Vector3(transform.position.x +1, transform.position.y, transform.position.z); break; 
            case dir.BOTH: startpos = new Vector3(transform.position.x, transform.position.y, transform.position.z); break; 
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(rend.isVisible) { timer++; }

        if(timer >= shootFreq)
        {
            timer = 0;
            if(ShootDir == dir.BOTH)
            {
                {
                    startpos.x += 1;
                    GameObject right = Instantiate(projectile, startpos, transform.rotation);

                    Rigidbody2D rightRB = right.GetComponent<Rigidbody2D>();
                    //Vector3 rightRBVel = rightRB.velocity;
                    rightRB.velocity += new Vector2(shootSpeed,0);
                }

                startpos.x -=2;
                GameObject left = Instantiate(projectile, startpos, transform.rotation);

                Rigidbody2D leftRB = left.GetComponent<Rigidbody2D>();
                //Vector3 leftRBVel = leftRB.velocity;
                leftRB.velocity += new Vector2(-shootSpeed, 0);

            }
            else
            {
                GameObject proj = Instantiate(projectile, startpos, transform.rotation);

                Rigidbody2D projRB = proj.GetComponent<Rigidbody2D>();
                //Vector3 projRBVel = projRB.velocity;

                int s = ShootDir == dir.LEFT ? -1 : 1;

                projRB.velocity += new Vector2(s * shootSpeed, 0);
            }
        }
    }
}
