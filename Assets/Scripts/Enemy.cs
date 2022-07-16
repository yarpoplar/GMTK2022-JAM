using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AnimationState { Idle, Walk, Hit, Dead };

public class Enemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent navMeshAgent;
    public GameObject spriteRoot;
    public GameObject sprite;
    public float Health = 5;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if ((!spriteRoot) || (!sprite))
        {
            Debug.LogError("Set sprite/spriteRoot for enemy!");
        }
        //navMeshAgent.velocity
    }

    private void Update()
    {
        navMeshAgent.destination = GameManager.Instance.Player.transform.position;
    }

    private void LateUpdate()
    {
        //sprite.transform.LookAt(Camera.main.transform.position, -Vector3.up);
        spriteRoot.transform.forward = Camera.main.transform.forward;
    }

    public void ApplyDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
            Destroy(gameObject);
    }
}
