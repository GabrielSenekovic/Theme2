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

    List<Vector3> contactPoints = new List<Vector3>();

    Animator anim;

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
	}
	
	void Update ()
    {
        Vector2 v2GroundedBoxCheckPosition = (Vector2)transform.position + new Vector2(0, -0.02f);
        Vector2 v2GroundedBoxCheckScale = (Vector2)transform.localScale + new Vector2(-0.02f, 0);
        Collider2D[] hit = Physics2D.OverlapBoxAll(v2GroundedBoxCheckPosition, v2GroundedBoxCheckScale, 0, lmWalls);
        bGrounded = false;
        if(hit.Length > 0)
        {
            contactPoints.Clear();
            for (int i = 0; i < hit.Length; i++)
            {
                List<ContactPoint2D> contacts = new List<ContactPoint2D>();
                hit[i].GetContacts(contacts);
                for (int j = 0; j < contacts.Count; j++)
                {
                    contactPoints.Add(contacts[j].point);
                    //Debug.Log((contacts[i].point - (Vector2)transform.position).normalized);
                    Vector2 normal = (contacts[j].point - v2GroundedBoxCheckPosition).normalized;
                    if (Mathf.Abs(normal.x) < 0.5f && normal.y < 0)
                    {
                        bGrounded = true;
                    }
                }
            }
        }
        //Physics2D.OverlapBox(v2GroundedBoxCheckPosition, v2GroundedBoxCheckScale, 0, lmWalls);

        groundedRemember -= Time.deltaTime;
        if (bGrounded)
        {
            groundedRemember = groundedRememberTime;
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            Collider2D[] check = Physics2D.OverlapCircleAll(transform.position, 1);
            for(int i = 0; i < check.Length; i++)
            {
                if(check[i].gameObject.CompareTag("Door"))
                {
                    check[i].gameObject.GetComponent<Door>().Open();
                }
            }
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
        anim.SetBool("Walking", Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0);
    }

    private void LateUpdate() 
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        for(int i = 0; i < contactPoints.Count; i++)
        {
            Gizmos.DrawSphere(contactPoints[i], 0.1f);
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(0, -0.02f), (Vector2)transform.localScale + new Vector2(-0.02f, 0));
        */
    }
}