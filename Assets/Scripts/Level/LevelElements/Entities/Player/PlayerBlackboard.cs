using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[System.Serializable]
public class PlayerBlackboard
{
    public float jumpPressedRemember = 0;
    [SerializeField]
    public float jumpPressedRememberTime;

    public float groundedRemember = 0;
    [SerializeField]
    public float groundedRememberTime;

    
    [Range(0, 1)]
    public float horizontalDampingBasic;
    [Range(0, 1)]
    public float horizontalDampingWhenStopping;
    [Range(0, 1)]
    public float horizontalDampingWhenTurning;

    [Range(0, 1)]
    public float verticalDampingBasic;
    [Range(0, 1)]
    public float verticalDampingWhenStopping;
    [Range(0, 1)]
    public float verticalDampingWhenTurning;

    [Range(0, 1)]
    public float cutJumpHeight;

    public float speed;
    public float jumpVelocity;

    public Rigidbody2D body;
    public Transform trans;
    public Transform visualsTrans;
    public LayerMask layerMask;
    public Animator anim;

    public bool CheckGrounded()
    {
        List<Vector3> contactPoints = new List<Vector3>();

        groundedRemember -= Time.deltaTime;

        Vector2 v2GroundedBoxCheckPosition = (Vector2)trans.position + new Vector2(0, -0.02f);
        Vector2 v2GroundedBoxCheckScale = (Vector2)trans.localScale + new Vector2(-0.02f, 0);
        Collider2D[] hit = Physics2D.OverlapBoxAll(v2GroundedBoxCheckPosition, v2GroundedBoxCheckScale, 0, layerMask);

        bool grounded = false;
        if (hit.Length > 0)
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
                        groundedRemember = groundedRememberTime;
                        grounded = true;
                    }
                }
            }
        }
        return grounded;
    }
}
