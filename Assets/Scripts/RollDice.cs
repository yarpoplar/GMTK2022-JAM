using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    private bool bRollFinished;
    private Rigidbody rbody;

    void Start() 
    {
        rbody = gameObject.GetComponent<Rigidbody>();
        rbody.AddForce(Random.insideUnitSphere * 10, ForceMode.Impulse);
        rbody.AddTorque(Random.insideUnitSphere * 10, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((!bRollFinished) && (rbody.IsSleeping()))
        {
            bRollFinished = true;
            Debug.Log(GetRollResult().GetComponent<RollSide>().Name);
        }
    }

    GameObject GetRollResult()
    {
        Transform resultTransform = null;
        float maxDot = -2;
        foreach(Transform child in transform)
        {
            float dot = Vector3.Dot(child.position - gameObject.transform.position, Vector3.up);
            if (dot > maxDot)
            {
                resultTransform = child;
                maxDot = dot;
            }
        }
        return resultTransform.gameObject;
    }
}
