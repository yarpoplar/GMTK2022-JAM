using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

enum AnimationState { Idle, Walk, Hit, Dead };

public class Enemy : MonoBehaviour, IDamageable
{
    private NavMeshAgent navMeshAgent;
    public GameObject spriteRoot;
    public GameObject sprite;
    private Animator animator;
    private Material spriteMat;
    private Tween hitTween;
    public float Health = 5;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if ((!spriteRoot) || (!sprite))
        {
            Debug.LogError("Set sprite/spriteRoot for enemy!");
        }
        //navMeshAgent.velocity
        spriteMat = sprite.GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        navMeshAgent.destination = GameManager.Instance.Player.transform.position;
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
    }

    private void LateUpdate()
    {
        spriteRoot.transform.forward = Camera.main.transform.forward;
    }

    public void ApplyDamage(float damage)
    {
        Health -= damage;
        spriteMat.SetFloat("_Hit", 1);
        hitTween.Kill();
        StartCoroutine(LateHitGlow());
        float testFloat = 0;
        DOTween.To(() => testFloat, x => testFloat = x, 52, 1);
        if (Health <= 0)
            Destroy(gameObject);
    }

    IEnumerator LateHitGlow()
    {
        yield return new WaitForSeconds(0.1f);
        hitTween = spriteMat.DOFloat(0, "_Hit", 0.15f);
    }
}
