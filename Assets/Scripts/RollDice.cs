using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

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
        rbody.AddForce(Random.insideUnitSphere * Random.Range(10, 20) + Vector3.up * Random.Range(30, 60), ForceMode.Impulse);
        rbody.AddTorque(Random.insideUnitSphere * Random.Range(10, 60), ForceMode.Impulse);

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((!bRollFinished) && (rbody.IsSleeping()))
        {
            bRollFinished = true;
            GetRollResult();
        }

        //rbody.AddForce(new Vector3(0, -1, 0) * 10 * rbody.mass, ForceMode.Force);
    }

    public void GetRollResult()
    {
        Transform resultTransform = null;
        float maxDot = -2;
        int i = 0;
        int resultIndex = 0;
        foreach (Transform child in transform)
        {
            i++;
            float dot = Vector3.Dot(child.position - gameObject.transform.position, Vector3.up);
            if (dot > maxDot)
            {
                resultTransform = child;
                maxDot = dot;
                resultIndex = i;
                
            }
        }
        rollEvents[resultIndex - 1].Invoke(resultIndex);
        Destroy(gameObject, 3f);
    }
}
