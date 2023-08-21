using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour, IPickupable
{
    Rigidbody2D body;
    CircleCollider2D circle;
    BoxCollider2D trigger;

    bool thrown;

    float explosionSensitivityTimer; //Within this window, the TNT cannot be exploded by contact
    float explosionSensitivityTimerMax = 0.2f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        trigger = GetComponent<BoxCollider2D>();
        thrown = false;
    }
    private void Update()
    {
        if(explosionSensitivityTimer < explosionSensitivityTimerMax)
        {
            explosionSensitivityTimer += Time.deltaTime;
        }
    }
    public GameObject GameObject()
    {
        return gameObject;
    }

    public void OnLetGo()
    {
    }

    public void OnPickUp()
    {
        body.velocity = Vector2.zero;
        body.simulated = false;
        trigger.enabled = false;
        circle.enabled = false;
    }

    public bool UseItem(int dir)
    {
        body.simulated = true;
        body.constraints = RigidbodyConstraints2D.None;
        body.AddForce(new Vector2(dir, 0) * 10, ForceMode2D.Impulse);
        thrown = true;
        explosionSensitivityTimer = 0;
        trigger.enabled = true;
        circle.enabled = true;
        return true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(body.velocity.magnitude > 0.5f && thrown && explosionSensitivityTimer >= explosionSensitivityTimerMax)
        {
            Explode();
        }
    }
    void Explode()
    {
        Destroy(gameObject);
    }
}
