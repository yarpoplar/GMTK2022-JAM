using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Task_FindFlag : TaskBase
{
    [SerializeField]
    public GameObject FlagPrefab;
    protected override void Start()
    {
        base.Start();
        Counter = "";
        PostCounter = "";

        NavMeshHit myNavHit;
        Vector3 randomSphere = Vector3.Scale(Random.onUnitSphere, new Vector3(1, 0, 1)).normalized * 40;
        if (NavMesh.SamplePosition(GameManager.Instance.Player.transform.position + randomSphere, out myNavHit, 300, -1))
        {
            Instantiate(FlagPrefab, myNavHit.position, Quaternion.identity);
        }
    }
}
