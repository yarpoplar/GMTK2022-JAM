using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_DoRoll : TaskBase
{
    public float RollsAmount = 15f;
    private float currentRollsAmount;

    protected override void Start()
    {
        base.Start();
        PostCounter = " / " + RollsAmount + " rolls";
        player.OnDash += CalculateRoll;
    }

    private void CalculateRoll()
    {
        currentRollsAmount++;
        Counter = currentRollsAmount.ToString("F0");
        if (currentRollsAmount >= RollsAmount)
        {
            CompleteTask();
            player.OnDash -= CalculateRoll;
        }
            
    }
}
