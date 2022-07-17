using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBase : MonoBehaviour
{
    [SerializeField]
    public string Name = "Task name";
    private bool TaskCompleted = false;
    private Player player;

    protected virtual void Start()
    {
        GameManager.Instance.TaskInit(this);
        player = GameManager.Instance.Player.GetComponent<Player>();
    }


    protected virtual void CompleteTask()
    {
        if(!TaskCompleted)
        {
            TaskCompleted = true;
            GameManager.Instance.TaskCompleted();
        }
    }
}
