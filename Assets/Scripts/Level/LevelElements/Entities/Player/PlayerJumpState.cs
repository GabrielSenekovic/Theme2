using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerJumpState : IState
{
    PlayerBlackboard blackboard;
    public int GetID() => (int)PlayerState.JUMPING;
    public PlayerJumpState(PlayerBlackboard blackboard)
    {
        this.blackboard = blackboard;
    }
    public void Enter()
    {
        Debug.Log("Entered Jump state");
        //blackboard.body.gravityScale = 2;
    }

    public void Exit()
    {
    }
    public int Update()
    {
        return 0;
    }

    public int FixedUpdate()
    {
        blackboard.jumpPressedRemember -= Time.deltaTime;

        if (blackboard.CheckGrounded() && blackboard.jumpPressedRemember < 0)
        {
            return (int)PlayerState.WALKING;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (blackboard.body.velocity.y > 0)
            {
                blackboard.body.velocity = new Vector2(blackboard.body.velocity.x, blackboard.body.velocity.y * blackboard.cutJumpHeight);
            }
        }
        float horizontalVelocity = blackboard.body.velocity.x;
        float rawHorizontal = Input.GetAxisRaw("Horizontal");
        if (rawHorizontal < 0) { blackboard.visualsTrans.localScale = new Vector3(-1, 1, 1); }
        if (rawHorizontal > 0) { blackboard.visualsTrans.localScale = new Vector3(1, 1, 1); }
        horizontalVelocity += rawHorizontal * blackboard.speed;
        

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

        int signBody = Mathf.RoundToInt(Mathf.Sign(blackboard.body.velocity.x));
        int signInput = Mathf.RoundToInt(rawHorizontal);
        bool ifSameSign = (Mathf.Abs(blackboard.body.velocity.x) < Mathf.Abs(horizontalVelocity) &&
            signBody == signInput);
        bool ifDifferentSign = signInput == -1 ? signBody != signInput && blackboard.body.velocity.x > horizontalVelocity 
                                               : signBody != signInput && blackboard.body.velocity.x <= horizontalVelocity;
        if (ifSameSign || 
            ifDifferentSign)
        { 
            blackboard.body.velocity = new Vector2(horizontalVelocity, blackboard.body.velocity.y);
        }
        return 0;
    }
}
