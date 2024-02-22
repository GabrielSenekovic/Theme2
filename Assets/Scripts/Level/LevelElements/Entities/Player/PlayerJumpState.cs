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
        return 0;
    }
}
