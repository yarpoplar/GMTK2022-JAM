using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_DoNOTRoll : TaskBase
{
    public float NotRollsTime = 15f;
    private float currentNotRollsTime;

    protected override void Start()
    {
        base.Start();
        PostCounter = " / " + NotRollsTime + " sec";
        player.OnDash += CalculateRoll;
    }

    private void Update()
    {
        currentNotRollsTime += Time.deltaTime;
        Counter = currentNotRollsTime.ToString("F0");
        if (currentNotRollsTime >= NotRollsTime)
        {
            CompleteTask();
            player.OnDash -= CalculateRoll;
        }
    }

    private void CalculateRoll()
    {
        currentNotRollsTime = 0; 
    }
}
