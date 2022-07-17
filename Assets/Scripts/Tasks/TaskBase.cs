using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase : MonoBehaviour
{
    [SerializeField]
    public string Name = "Task name";
    [HideInInspector]
    public string Counter = "0";
    [HideInInspector]
    public string PostCounter = "/10";
    private bool TaskCompleted = false;
    protected Player player;

    protected virtual void Start()
    {
        //GameManager.Instance.TaskInit(this);
        player = GameManager.Instance.Player.GetComponent<Player>();
    }


    public virtual void CompleteTask()
    {
        if(!TaskCompleted)
        {
            TaskCompleted = true;
            GameManager.Instance.TaskCompleted();
            Destroy(this);
        }
    }
}
