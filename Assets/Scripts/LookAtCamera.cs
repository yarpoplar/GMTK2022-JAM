using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField]
    private bool isWeapon = false;

    void LateUpdate()
    {
        if (isWeapon)
            transform.forward = Camera.main.transform.forward;
        else
            transform.forward = Camera.main.transform.forward;
    }
}
