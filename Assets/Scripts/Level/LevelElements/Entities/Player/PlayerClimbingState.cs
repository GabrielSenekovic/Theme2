using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerClimbingState : IState
{
    PlayerBlackboard blackboard;

    public int GetID() => (int)PlayerState.CLIMBING;
    public PlayerClimbingState(PlayerBlackboard blackboard)
    {
        this.blackboard = blackboard;
    }
    public void Enter()
    {
        blackboard.body.velocity = new Vector2(0, 0);
        blackboard.body.gravityScale = 0;
    }
    public void Exit()
    {
    }

    public int Update()
    {
        Collider2D[] checkRope = Physics2D.OverlapCircleAll(blackboard.trans.position, 0.5f);
        if (!checkRope.Any(r => r.gameObject.CompareTag("Rope")))
        {
            blackboard.body.gravityScale = 2;
            return (int)PlayerState.WALKING;
        }
        return (int)PlayerState.NONE;
    }

    public int FixedUpdate()
    {
        float horizontalVelocity = blackboard.body.velocity.x;
        float verticalVelocity = blackboard.body.velocity.y;
        float rawVertical = Input.GetAxisRaw("Vertical");

        verticalVelocity += rawVertical * blackboard.speed;

        if (Mathf.Abs(rawVertical) < 0.01f)
        {
            verticalVelocity *= Mathf.Pow(1f - blackboard.verticalDampingWhenStopping, Time.deltaTime * 20f);
        }
        else if (Mathf.Sign(rawVertical) != Mathf.Sign(verticalVelocity))
        {
            verticalVelocity *= Mathf.Pow(1f - blackboard.verticalDampingWhenTurning, Time.deltaTime * 20f);
        }
        else
        {
            verticalVelocity *= Mathf.Pow(1f - blackboard.verticalDampingBasic, Time.deltaTime * 20f);
        }
        blackboard.body.velocity = new Vector2(horizontalVelocity, verticalVelocity);

        return 0;
    }
}
