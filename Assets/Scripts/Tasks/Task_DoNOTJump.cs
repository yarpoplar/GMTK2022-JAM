using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_DoNOTJump : TaskBase
{
    public float NotJumpTime = 10f;
    private float currentNotJumpTime;

    protected override void Start()
    {
        base.Start();
        PostCounter = " / " + NotJumpTime + " sec";
    }

    private void Update()
    {
        currentNotJumpTime += Time.deltaTime;
        Counter = currentNotJumpTime.ToString("F0");
        if (currentNotJumpTime >= NotJumpTime)
        {
            CompleteTask();
        }
    }
}
