using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heron : MonoBehaviour
{
    [SerializeField] GameObject head;
    [SerializeField] List<GameObject> neckSegments;
    [SerializeField] GameObject body;
    [SerializeField] Rigidbody2D headBody;
    [SerializeField] SpringJoint2D springJoint;

    float maximumDistanceBetweenSegments = 0.7f;
    [SerializeField] float headSwoopSpeed;

    GameObject player;

    private void Awake()
    {
        springJoint.distance = maximumDistanceBetweenSegments;
    }

    public float MaxLength()
    {
        return maximumDistanceBetweenSegments * (neckSegments.Count + 1);
    }

    private void Update()
    {
        if(player)
        {
            if((body.transform.position - head.transform.position).magnitude < MaxLength())
            {
                Vector2 direction = (player.transform.position - head.transform.position).normalized;
                headBody.AddForce(direction * headSwoopSpeed, ForceMode2D.Impulse);
            }
        }
        AdjustNeck();
    }
    void AdjustNeck()
    {
        Vector3 positionIncrement = (head.transform.position - body.transform.position) / (float)(neckSegments.Count + 1);
        for(int i = 0; i < neckSegments.Count; i++)
        {
            neckSegments[i].transform.position = body.transform.position + positionIncrement * (i + 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }
}
