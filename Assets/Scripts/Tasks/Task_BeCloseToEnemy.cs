using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_BeCloseToEnemy : TaskBase
{
    public float StaySeconds = 10f;
    public float Radius = 4f;
    private float currentStaySeconds;
    protected override void Start()
    {
        base.Start();
        //rbody = player.gameObject.GetComponent<Rigidbody>();
        PostCounter = " / " + StaySeconds;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Enemy");
        //if (Physics.SphereCast(GameManager.Instance.Player.transform.position, 10, Vector3.up, out hit, Mathf.Infinity, mask))
        //{
        //    Debug.Log(hit.collider.gameObject.transform.root.gameObject.name);
        //    currentStaySeconds += Time.fixedDeltaTime;
        //}
        Collider[] col = Physics.OverlapSphere(GameManager.Instance.Player.transform.position, Radius, mask);
        if (col.Length > 0)
        {
            currentStaySeconds += Time.fixedDeltaTime;
            if (currentStaySeconds >= StaySeconds)
                CompleteTask();
        }
        Counter = currentStaySeconds.ToString("F1");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GameManager.Instance.Player.transform.position, Radius);
    }
}
