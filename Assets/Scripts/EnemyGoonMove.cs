using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyGoonMove : MonoBehaviour
{
    public float movementSpeed;
    public float raycastingDistance = 0.5f;

    bool goingRight;
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
            directionTranslation *= Time.deltaTime * movementSpeed;

            transform.Translate(directionTranslation);

            CheckForWalls();
    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (goingRight) ? Vector3.right : Vector3.left;
        RaycastHit2D hit = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.07f, 0), raycastDirection, raycastingDistance)
            .FirstOrDefault(h => h.transform.CompareTag("Tilemap"));


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
