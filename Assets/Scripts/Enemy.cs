using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

enum AnimationState { Idle, Walk, Hit, Dead };

public class Enemy : MonoBehaviour, IDamageable
{
    public float Health = 5;
    public float Damage = 1;
    public float KnockbackForce = 2;

    public GameObject spriteRoot;
    public GameObject sprite;

    public Weapon EnemyWeapon;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Material spriteMat;
    private Tween hitTween;
    private Rigidbody rbody;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if ((!spriteRoot) || (!sprite))
        {
            Debug.LogError("Set sprite/spriteRoot for enemy!");
        }
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        spriteMat = spriteRenderer.material;
        rbody = GetComponent<Rigidbody>();
    }

    //void Start()
    //{
    //    StartCoroutine(ShootRoutine());
    //}

    //IEnumerator ShootRoutine()
    //{
    //    yield return new WaitForSeconds(EnemyWeapon.fireSpeed);
    //}

    private void Update()
    {
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        EnemyWeapon.gameObject.transform.LookAt(new Vector3(playerPos.x, transform.position.y, playerPos.z));
    }

    private void FixedUpdate()
    {
        if (navMeshAgent.enabled)
            navMeshAgent.destination = GameManager.Instance.Player.transform.position;
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        if (navMeshAgent.velocity.magnitude >= 0.1)
            spriteRenderer.flipX = Vector3.Dot(Vector3.right, navMeshAgent.velocity.normalized) < 0;

        EnemyWeapon.Shoot();
    }

    private void LateUpdate()
    {
        //spriteRoot.transform.forward = Camera.main.transform.forward;
    }

    public void ApplyDamage(float damage, Vector3 knockback)
    {
        knockback.y = 0;
        StartCoroutine(HitRoutine(knockback));

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

    private IEnumerator HitRoutine(Vector3 knockback)
    {
        navMeshAgent.enabled = false;
        navMeshAgent.velocity = Vector3.zero;
        float timeLeft = 0f;
        while (timeLeft <= 0.2f)
        {
            timeLeft += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            transform.position += knockback * Time.fixedDeltaTime * (1 - timeLeft / 0.2f);
            //rb.AddForce(knockback * Time.fixedDeltaTime * (1 - timeLeft / 0.2f), ForceMode.VelocityChange);
        }
        navMeshAgent.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.root.gameObject.name);
        GameObject target = other.transform.root.gameObject;
        if (target.CompareTag("Player"))
        {
            if (target.TryGetComponent(out IDamageable enemy))
                enemy.ApplyDamage(Damage, (target.transform.position - transform.position).normalized * KnockbackForce);

        }
    }
}
