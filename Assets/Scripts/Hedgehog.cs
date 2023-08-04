using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Hedgehog : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public bool goingRight = true;

    public float raycastingDistance = 1f;

    private SpriteRenderer spriteRenderer;

    public bool isStanding = false;

    public Sprite[] spriteStates;
    public GameObject colliderOB;
    public KillPlayer hostile;
    public CircleCollider2D vision;
    public float angle = 0;
    private GameObject player;
    private bool enteredGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = goingRight;
        hostile = GetComponent<KillPlayer>();
        player = UIManager.Instance.player.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStanding)
        {
            Vector3 directionTranslation = (goingRight) ? transform.right : -transform.right;
            directionTranslation *= Time.deltaTime * movementSpeed;

            transform.Translate(directionTranslation);

            CheckForWalls();
        }
    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (goingRight) ? Vector3.right : Vector3.left;
        RaycastHit2D hit = Physics2D.RaycastAll(transform.position + raycastDirection * raycastingDistance - new Vector3(0f, 0.25f, 0f), raycastDirection, 0.075f)
            .FirstOrDefault(h=>h.transform.CompareTag("Tilemap"));

        if (hit.collider != null)
        {
            if (hit.transform.tag == "Tilemap")
            {
                goingRight = !goingRight;
                spriteRenderer.flipX = goingRight;

            }
        }
    }

    private float AngleDeg(Vector2 point)
    {
        return Mathf.Rad2Deg * Mathf.Atan2(point.y - transform.position.y, point.x - transform.position.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enteredGrounded = player.GetComponent<PlayerMovement>().bGrounded;
            angle = (AngleDeg(collision.ClosestPoint(transform.position)) +360)%360;
            Debug.Log("angle: " + angle);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float radius = enteredGrounded ? 80 : 20;
            if( (angle > 90 - radius && angle < 90 + radius))// || (player.transform.position.x - transform.position.x) > 3 )
            {
                isStanding = false;
                spriteRenderer.sprite = spriteStates[0];
                hostile.isOn = true;
            }
            else
            {
                isStanding = true;
                spriteRenderer.sprite = spriteStates[1];
                hostile.isOn = false;
            }
         
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isStanding = false;
            spriteRenderer.sprite = spriteStates[0];
            hostile.isOn = true;
        }
    }
}
