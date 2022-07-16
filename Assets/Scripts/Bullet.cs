using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed = 4000f;
    [SerializeField]
    private float Damage = 1;
    [SerializeField]
    private float Knockback = 2f;
    [SerializeField]
    private float Livetime = 4f;
    [Space]
    [SerializeField]
    private Rigidbody rb;

    void Start()
    {
        rb.AddForce(transform.forward * Speed, ForceMode.Acceleration);
        Destroy(gameObject, Livetime);
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.TryGetComponent(out IDamageable enemy))
            enemy.ApplyDamage(Damage, (other.transform.position - GameManager.Instance.Player.transform.position).normalized * Knockback);

        Destroy(gameObject);	
	}
}
