using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System.Linq;

public class Player : MonoBehaviour
{
    public bool dead = false;

    public VisualEffect breakVFX;
    public SpriteRenderer renderer;

    public AudioClip death;

    [SerializeField] PlayerBlackboard blackboard;

    private StateMachine stateMachine;

    void Start ()
    {
        breakVFX.enabled = false;

        stateMachine = new StateMachine();
        stateMachine.Add(new PlayerWalkingState(blackboard));
        stateMachine.Add(new PlayerClimbingState(blackboard));
        stateMachine.Add(new PlayerJumpState(blackboard));

        stateMachine.SetState((int)PlayerState.WALKING);
    }
	
    private void Update() 
    {
        if (dead)
        {
            blackboard.body.velocity = new Vector2(0, 0);
            return;
        }

        stateMachine.Update();
    }

	void FixedUpdate ()
    {
        if (dead)
        {
            blackboard.body.velocity = new Vector2(0, 0);
            return;
        }

        stateMachine.FixedUpdate();
    }
    public StateMachine GetStateMachine() => stateMachine;
    public int GetDirection()
    {
        return Mathf.RoundToInt(blackboard.visualsTrans.localScale.x);
    }
    public void Die()
    {
        if (!dead)
        {
            breakVFX.enabled = true;
            breakVFX.Play();
            renderer.color = Color.clear;
            dead = true;
            AudioManager.PlaySound(death);
            UIManager.Instance.Reset();
            SceneLoader.Instance.Reload();
        }
    }
}