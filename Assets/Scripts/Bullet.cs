using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed = 4000f;
    [SerializeField]
    private float Damage = 1;
    [Space]
    [SerializeField]
    private Rigidbody rb;

    void Start()
    {
        rb.AddForce(transform.forward * Speed, ForceMode.Acceleration);
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.TryGetComponent(out IDamageable enemy))
            enemy.ApplyDamage(Damage);

        Destroy(gameObject);	
	}
}
