using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_RunForMeters : TaskBase
{
    public float RunMeters = 15f;
    private float currentRunMeters;

    public bool PlayerInPlace = false;
    private Rigidbody rbody;

    protected override void Start()
    {
        base.Start();
        rbody = player.gameObject.GetComponent<Rigidbody>();
        PostCounter = " / " + RunMeters + " meters";
    }

    private void FixedUpdate()
    {
        if (currentRunMeters <= RunMeters)
        {
            if (rbody.velocity.magnitude > 0)
                currentRunMeters += rbody.velocity.magnitude * Time.deltaTime * 0.1f;
        }
        else
        {
            CompleteTask();
        }
        
        Counter = currentRunMeters.ToString("F0");
    }
}
