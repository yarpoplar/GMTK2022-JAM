using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_StayForSeconds : TaskBase
{
    public float StaySeconds = 5f;
    private float currentStaySeconds;

    public bool PlayerInPlace = false;
    private Rigidbody rbody;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rbody = player.gameObject.GetComponent<Rigidbody>();
        PostCounter = " / " + StaySeconds;
    }

    private void FixedUpdate()
    {
        //Check if in place
        if (PlayerInPlace)
        {
            if (rbody.velocity.magnitude > 0.1)
                PlayerInPlace = false;

            currentStaySeconds += Time.deltaTime;
            if (currentStaySeconds >= StaySeconds)
            {
                CompleteTask();
            }
        }
        else
        {
            if (rbody.velocity.magnitude <= 0.1)
                PlayerInPlace = true;

            currentStaySeconds = 0;
        }
        Counter = currentStaySeconds.ToString("F1");
    }
}
