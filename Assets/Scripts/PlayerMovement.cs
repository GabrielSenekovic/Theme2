using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    LayerMask lmWalls;
    [SerializeField]
    float jumpVelocity = 5;
    public bool dead = false;
    public bool climbing = false;

    Rigidbody2D rigid;

    float jumpPressedRemember = 0;
    [SerializeField]
    float jumpPressedRememberTime;

    float groundedRemember = 0;
    [SerializeField]
    float groundedRememberTime;

    [SerializeField]
    [Range(0, 1)]
    float horizontalDampingBasic;
    [SerializeField]
    [Range(0, 1)]
    float horizontalDampingWhenStopping;
    [SerializeField]
    [Range(0, 1)]
    float horizontalDampingWhenTurning;

    [SerializeField]
    [Range(0, 1)]
    float verticalDampingBasic;
    [SerializeField]
    [Range(0, 1)]
    float verticalDampingWhenStopping;
    [SerializeField]
    [Range(0, 1)]
    float verticalDampingWhenTurning;

    [SerializeField]
    [Range(0, 1)]
    float cutJumpHeight;

    [SerializeField]
    public float speed;

    [SerializeField]
    public bool bGrounded;

    List<Vector3> contactPoints = new List<Vector3>();

    Animator anim;
    public VisualEffect breakVFX; 
    public SpriteRenderer renderer;

    [SerializeField] Transform visualsTransform;

    public AudioClip death;

    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        breakVFX.enabled = false;
	}
	
    private void Update() 
    {
        if (dead)
        {
            rigid.velocity = new Vector2(0, 0);
            return;
        }

        if(Input.GetKeyDown(KeyCode.W) && bGrounded)
        {
            Collider2D[] checkDoor = Physics2D.OverlapCircleAll(transform.position, 1);
            for(int i = 0; i < checkDoor.Length; i++)
            {
                if(checkDoor[i].gameObject.CompareTag("Door"))
                {
                    checkDoor[i].gameObject.GetComponent<Door>().Open();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && !climbing)
        {
            Collider2D[] checkRope = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            if (checkRope.Any(r => r.gameObject.CompareTag("Rope")))
            {
                climbing = true;
                rigid.velocity = new Vector2(0, 0);
                rigid.gravityScale = 0;
            }
        }
        else if (climbing)
        {
            Collider2D[] checkRope = Physics2D.OverlapCircleAll(transform.position, 0.5f);
            if (!checkRope.Any(r => r.gameObject.CompareTag("Rope")))
            {
                climbing = false;
                rigid.gravityScale = 2;
            }
        }

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
    }

	void FixedUpdate ()
    {
        if (dead)
        {
            rigid.velocity = new Vector2(0, 0);
            return;
        }

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
                    Vector2 normal = (contacts[j].point - v2GroundedBoxCheckPosition).normalized;
                    if (Mathf.Abs(normal.x) < 0.5f && normal.y < 0)
                    {
                        bGrounded = true;
                    }
                }
            }
        }

        groundedRemember -= Time.deltaTime;
        if (bGrounded)
        {
            groundedRemember = groundedRememberTime;
        }

        jumpPressedRemember -= Time.deltaTime;

        if ((jumpPressedRemember > 0) && (groundedRemember > 0) && rigid.velocity.y == 0)
        {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            rigid.velocity = new Vector2(rigid.velocity.x * 1.5f, jumpVelocity);
        }

        float horizontalVelocity = rigid.velocity.x;
        float verticalVelocity = rigid.velocity.y;
        float rawHorizontal = Input.GetAxisRaw("Horizontal");
        float rawVertical = Input.GetAxisRaw("Vertical");
        horizontalVelocity += rawHorizontal * speed;
        if(rawHorizontal < 0) { visualsTransform.localScale = new Vector3(-1, 1,1);}
        if(rawHorizontal > 0) { visualsTransform.localScale = new Vector3(1, 1,1);}

        if (Mathf.Abs(rawHorizontal) < 0.01f)
        {
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingWhenStopping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(rawHorizontal) != Mathf.Sign(horizontalVelocity))
        {
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            horizontalVelocity *= Mathf.Pow(1f - horizontalDampingBasic, Time.deltaTime * 10f);
        }

        if(climbing)
        {
            verticalVelocity += rawVertical * speed;

            if (Mathf.Abs(rawVertical) < 0.01f)
            {
                verticalVelocity *= Mathf.Pow(1f - verticalDampingWhenStopping, Time.deltaTime * 20f);
            }
            else if (Mathf.Sign(rawVertical) != Mathf.Sign(verticalVelocity))
            {
                verticalVelocity *= Mathf.Pow(1f - verticalDampingWhenTurning, Time.deltaTime * 20f);
            }
            else
            {
                verticalVelocity *= Mathf.Pow(1f - verticalDampingBasic, Time.deltaTime * 20f);
            }
        }


        rigid.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        anim.SetBool("Walking", Mathf.Abs(rawHorizontal) > 0);
    }
    public int GetDirecton()
    {
        return Mathf.RoundToInt(visualsTransform.localScale.x);
    }
    public void Die()
    {
        if (!dead)
        {
            breakVFX.enabled = true;
            breakVFX.Play();
            renderer.color = Color.clear;
            dead = true;
            AudioManager.PlaySound(death);
            UIManager.Instance.Reset();
            SceneLoader.Instance.Reload();
        }
    }
}