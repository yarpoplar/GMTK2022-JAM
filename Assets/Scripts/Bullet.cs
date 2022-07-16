using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletForce = 500f;
    [Space]
    [SerializeField]
    private Rigidbody rb;

    void Start()
    {
        rb.AddForce(transform.forward * bulletForce, ForceMode.Acceleration);
    }

	private void OnTriggerEnter(Collider other)
	{
        Destroy(gameObject);	
	}
}
