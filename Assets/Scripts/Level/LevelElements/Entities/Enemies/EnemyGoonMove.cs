using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyGoonMove : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float raycastingDistance = 0.5f;
    [SerializeField] bool fallOffLedge;

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
        if(!fallOffLedge)
        {
            CheckForLedge();
        }
    }

    private void CheckForWalls()
    {
        Vector3 raycastDirection = (goingRight) ? Vector3.right : Vector3.left;
        var hit = Physics2D.RaycastAll(transform.position - new Vector3(0, 0.07f, 0), raycastDirection, raycastingDistance);

        bool turnAround = hit.Any(h => h.transform.CompareTag("Tilemap") || 
        (h.collider.gameObject != gameObject && h.collider.gameObject.GetComponent<Rigidbody2D>()));

        if (!turnAround) { return; }
            
        foreach (var item in hit)
        {
            if(item.collider.gameObject.TryGetComponent(out EnemyGoonMove move))
            {
                move.TurnAround();
            }
        }
    }
    private void CheckForLedge()
    {
        Vector2 raycastDirection = goingRight ? Vector2.right : Vector2.left;
        Vector2 origin = transform.position - new Vector3(0.5f * -raycastDirection.x, 1.07f, 0);
        RaycastHit2D hit = Physics2D.RaycastAll(origin, raycastDirection, raycastingDistance)
            .FirstOrDefault(h => h.transform.CompareTag("Tilemap"));

        Debug.DrawLine(origin, origin + raycastDirection * raycastingDistance);

        if (hit.collider == null || (hit.collider != null && (hit.transform.tag != "Tilemap"))) 
        {
            TurnAround();
        }
    }
    void TurnAround()
    {
        goingRight = !goingRight;
        spriteRenderer.flipX = goingRight;
    }
}
