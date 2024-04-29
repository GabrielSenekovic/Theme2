using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static CameraPan;

public class PlayerWalkingState : IState
{
    PlayerBlackboard blackboard;
    public int GetID() => (int)PlayerState.WALKING;
    public PlayerWalkingState(PlayerBlackboard blackboard)
    {
        this.blackboard = blackboard;
    }
    public void Enter()
    {
        Debug.Log("Entered walking state");
    }

    public void Exit()
    {
    }

    public int Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Collider2D[] checkDoor = Physics2D.OverlapCircleAll(blackboard.trans.position, 1);
            for (int i = 0; i < checkDoor.Length; i++)
            {
                if (checkDoor[i].gameObject.CompareTag("Door"))
                {
                    checkDoor[i].gameObject.GetComponent<Door>().Open();
                }
            }

            Collider2D[] checkRope = Physics2D.OverlapCircleAll(blackboard.trans.position, 0.5f);
            if (checkRope.Any(r => r.gameObject.CompareTag("Rope")))
            {
                return (int)PlayerState.CLIMBING;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            blackboard.jumpPressedRemember = blackboard.jumpPressedRememberTime;
        }

        return 0;
    }

    public int FixedUpdate()
    {
        bool grounded = blackboard.CheckGrounded();
        bool startJump = CheckJump();

        if(!grounded || startJump)
        {
            return (int)PlayerState.JUMPING;
        }

        Walk();

        return 0;
    }
    
    bool CheckJump()
    {
        blackboard.jumpPressedRemember -= Time.deltaTime;

        if ((blackboard.jumpPressedRemember > 0) && blackboard.groundedRemember > 0)
        {
            blackboard.jumpPressedRemember = 0;
            blackboard.groundedRemember = 0;
            blackboard.body.velocity = new Vector2(blackboard.body.velocity.x * 1.5f, blackboard.jumpVelocity);
            return true;
        }
        return false;
    }
    void Walk()
    {
        float horizontalVelocity = blackboard.body.velocity.x;
        float verticalVelocity = blackboard.body.velocity.y;
        float rawHorizontal = Input.GetAxisRaw("Horizontal");
        horizontalVelocity += rawHorizontal * blackboard.speed;
        if (rawHorizontal < 0) { blackboard.visualsTrans.localScale = new Vector3(-1, 1, 1); }
        if (rawHorizontal > 0) { blackboard.visualsTrans.localScale = new Vector3(1, 1, 1); }

        if (Mathf.Abs(rawHorizontal) < 0.01f)
        {
            horizontalVelocity *= Mathf.Pow(1f - blackboard.horizontalDampingWhenStopping, Time.deltaTime * 10f);
        }
        else if (Mathf.Sign(rawHorizontal) != Mathf.Sign(horizontalVelocity))
        {
            horizontalVelocity *= Mathf.Pow(1f - blackboard.horizontalDampingWhenTurning, Time.deltaTime * 10f);
        }
        else
        {
            horizontalVelocity *= Mathf.Pow(1f - blackboard.horizontalDampingBasic, Time.deltaTime * 10f);
        }

        blackboard.body.velocity = new Vector2(horizontalVelocity, verticalVelocity);
        blackboard.anim.SetBool("Walking", Mathf.Abs(rawHorizontal) > 0);
    }
}
