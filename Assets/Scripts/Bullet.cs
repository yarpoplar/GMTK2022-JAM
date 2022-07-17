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
    [SerializeField]
    private GameObject VFX;

    [SerializeField]
    private bool isExplosive = false;
    [SerializeField]
    private float explosiveRadius = 1f;

    [SerializeField]
    private float destroyDelay = 1f;

    void Start()
    {
        rb.AddForce(transform.forward * Speed, ForceMode.Acceleration);
        Destroy(gameObject, Livetime);
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.transform.root.TryGetComponent(out IDamageable enemy))
        {
            //enemy.ApplyDamage(Damage, (Vector3.Scale(transform.position - other.transform.root.transform.position, new Vector3(1, 0, 1))).normalized * Knockback);
            enemy.ApplyDamage(Damage, (rb.velocity).normalized * Knockback);
        }

        Destroy(gameObject, destroyDelay);	
	}

    private void OnDestroy()
    {
        if (isExplosive)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosiveRadius);
            foreach (Collider hit in colliders)
                if (hit.TryGetComponent(out IDamageable toDamage))
                    toDamage.ApplyDamage(Damage);
        }

        if (VFX != null)
            Instantiate(VFX, transform.position, Quaternion.identity);
    }
}
