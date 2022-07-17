using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_StayForSeconds : TaskBase
{
    public float StaySeconds = 5f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

    }

    IEnumerator StaySecondsCheck()
    {
        yield return new WaitForSeconds(StaySeconds);
    }
}
