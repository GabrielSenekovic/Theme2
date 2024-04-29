using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Timer 
{
    [SerializeField] float timerMax;
    float timer;
    Action OnDone;

    public Timer(Action OnDone)
    {
        this.OnDone = OnDone;
    }
    public void Tick()
    {
        timer += Time.deltaTime;
        if(timer >= timerMax)
        {
            OnDone();
            timer = 0;
        }
    }
}
