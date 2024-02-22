using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public int GetID();
    public void Enter();
    public int Update();
    public int FixedUpdate();
    public void Exit();
}
