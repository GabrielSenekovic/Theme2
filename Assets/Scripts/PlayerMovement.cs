using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    LayerMask lmWalls;
    [SerializeField]
    float jumpVelocity = 5;

    Rigidbody2D rigid;

    float jumpPressedRemember = 0;
    [SerializeField]
    float jumpPressedRememberTime = 0.2f;

    float groundedRemember = 0;
    [SerializeField]
    float groundedRememberTime = 0.25f;

    [SerializeField]
    [Range(0, 1)]
    float horizontalDampingBasic = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float horizontalDampingWhenStopping = 0.5f;
    [SerializeField]
    [Range(0, 1)]
    float horizontalDampingWhenTurning = 0.5f;

    [SerializeField]
    [Range(0, 1)]
    float cutJumpHeight = 0.5f;

    [SerializeField]
    float speed;

    [SerializeField]
    public bool bGrounded;

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
        Vector2 v2GroundedBoxCheckPosition = (Vector2)transform.position + new Vector2(0, -0.02f);
        Vector2 v2GroundedBoxCheckScale = (Vector2)transform.localScale + new Vector2(-0.02f, 0);
        Collider2D hit = Physics2D.OverlapBox(v2GroundedBoxCheckPosition, v2GroundedBoxCheckScale, 0, lmWalls);
        bGrounded = false;
        List<ContactPoint2D> contacts = new List<ContactPoint2D>();
        if(hit != null)
        {
            hit.GetContacts(contacts);
            for(int i = 0; i < contacts.Count; i++)
            {
                //Debug.Log((contacts[i].point - (Vector2)transform.position).normalized);
                Vector2 normal = (contacts[i].point - (Vector2)transform.position).normalized;
                if(Mathf.Abs(normal.x) < 0.15f && normal.y < 0)
                {
                    bGrounded = true;
                }
            }
        }
        //Physics2D.OverlapBox(v2GroundedBoxCheckPosition, v2GroundedBoxCheckScale, 0, lmWalls);

        groundedRemember -= Time.deltaTime;
        if (bGrounded)
        {
            groundedRemember = groundedRememberTime;
        }

        jumpPressedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressedRemember = jumpPressedRememberTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * cutJumpHeight);
            }
        }

        if ((jumpPressedRemember > 0) && (groundedRemember > 0))
        {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpVelocity);
        }

        float horizontalVelocity = rigid.velocity.x;
        horizontalVelocity += Input.GetAxisRaw("Horizontal") * speed;

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f)
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(horizontalVelocity))
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);
        else
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingBasic, Time.deltaTime * 10f);

        rigid.velocity = new Vector2(horizontalVelocity, rigid.velocity.y);
    }

    private void LateUpdate() 
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
}