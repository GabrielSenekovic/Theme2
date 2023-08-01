using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    public bool goingRight = true;

    public float raycastingDistance = 1f;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = goingRight;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionTranslation = (goingRight) ? transform.right : -transform.right;
        directionTranslation *= Time.deltaTime *movementSpeed;

        transform.Translate(directionTranslation);


        CheckForWalls();
    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (goingRight) ? Vector3.right : Vector3.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position + raycastDirection * raycastingDistance - new Vector3(0f, 0.25f, 0f), raycastDirection, 0.075f);

        if (hit.collider != null)
        {
            if (hit.transform.tag == "Tilemap")
            {
                goingRight = !goingRight;
                spriteRenderer.flipX = goingRight;

            }
        }
    }
}
