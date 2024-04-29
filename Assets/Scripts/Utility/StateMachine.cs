using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StateMachine
{
    public List<IState> states = new List<IState>();

    public IState currentState;
    public void Add(IState state)
    {
        states.Add(state);
    }
    public void SetState(int state)
    {
        CheckSwitchState(state);
    }

    public void Update()
    {
        CheckSwitchState(currentState.Update());
    }
    public void FixedUpdate()
    {
        CheckSwitchState(currentState.FixedUpdate());
    }
    public void CheckSwitchState(int value)
    {
        if (value != 0)
        {
            IState state = states.FirstOrDefault(s => s.GetID() == value);
            if (state != null)
            {
                currentState?.Exit();
                currentState = state;
                currentState.Enter();
            }
            else
            {
                Debug.LogWarning("Tried to switch to state: " + value + " but it wasn't found in the state machine");
            }
        }
    }
    public int GetCurrentStateID() => currentState.GetID();
}
