using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPickup : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.transform.root.gameObject;
        if (target.CompareTag("Player"))
        {
            GameManager.Instance.CurrentTask.CompleteTask();
            Destroy(gameObject);
        }
    }
}
