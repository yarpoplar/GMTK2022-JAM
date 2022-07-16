using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RollDice : MonoBehaviour
{
    [System.Serializable]
    public class DiceEvent : UnityEvent<int>
    {
    }

    private bool bRollFinished;
    private Rigidbody rbody;

    [SerializeField]
    private List<DiceEvent> rollEvents;

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
            GetRollResult();
        }
    }

    public void GetRollResult()
    {
        Transform resultTransform = null;
        float maxDot = -2;
        int i = 0;
        foreach (Transform child in transform)
        {
            i++;
            float dot = Vector3.Dot(child.position - gameObject.transform.position, Vector3.up);
            if (dot > maxDot)
            {
                resultTransform = child;
                maxDot = dot;
                rollEvents[i - 1].Invoke(i);
                Destroy(gameObject, 3f);
            }
        }
    }
}
