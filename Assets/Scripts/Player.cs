using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour, IDamageable
{
    [Header("Setup")]
    [SerializeField]
    private GameObject GFX;
    [SerializeField]
    private GameObject DashVFX;
    [SerializeField]
    private GameObject Sprite;
    private SpriteRenderer spriteRenderer;
    [Space]
    [Header("Parameters")]
    [SerializeField]
    public float Health = 100;
    [SerializeField]
    public float MaxHealth = 100;
    [SerializeField]
    public float moveSpeed = 30f;
    [SerializeField]
    private float dashCooldown = 1f;
    [SerializeField]
    private float dashTime = .5f;
    [SerializeField]
    private float dashVelocity = 1000f;
    private Rigidbody rb = null;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Material spriteMat;
    private Tween hitTween;
    private Animator animator;

    public bool IsDead = false;

    private bool isDashing = false;
    private bool canDash = true;

    private AudioSource sfx;

    public delegate void OnDashDelegate();
    public event OnDashDelegate OnDash;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = Sprite.GetComponent<SpriteRenderer>();
        spriteMat = spriteRenderer.material;
        animator = GetComponent<Animator>();
        sfx = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isDashing)
            return;

        if (IsDead)
            return;

        // Movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
		//rb.AddForce(moveInput * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        moveVelocity = moveInput * moveSpeed;
        if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector3.zero && canDash)
            StartCoroutine(DashRoutine(moveInput));
    }

    private IEnumerator DashRoutine(Vector3 direction)
	{
        if (OnDash != null)
            OnDash();
        isDashing = true;
        canDash = false;
        float timeLeft = 0f;
        if (DashVFX != null)
            Instantiate(DashVFX, transform.position, Quaternion.identity);
        float dirModifier = Vector3.Dot(Vector3.right, direction) >= 0 ? 1 : -1;

        GFX.transform.DOScale(new Vector3(.8f, .8f, .8f), dashTime / 2);
        GFX.transform.DOLocalRotate(new Vector3(0, 0, 360 * dirModifier), dashTime, RotateMode.LocalAxisAdd);

        while (timeLeft <= dashTime * 0.75f)
		{
            timeLeft += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            rb.AddForce(direction * dashVelocity * (1 - timeLeft / dashTime * 0.75f) * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

        GFX.transform.DOScale(new Vector3(1, 1, 1), dashTime / 2);
        //GFX.transform.DOLocalRotate(new Vector3(0, 0, 360), dashTime / 2);

        //while (timeLeft <= (dashTime / 2))
        //{
        //    timeLeft += Time.fixedDeltaTime;
        //    yield return new WaitForFixedUpdate();
        //    rb.AddForce(direction * dashVelocity * Time.fixedDeltaTime, ForceMode.VelocityChange);
        //}


        isDashing = false;

        yield return new WaitForSeconds(dashCooldown - dashTime);

        canDash = true;

    }

	private void FixedUpdate()
	{
        rb.velocity = moveVelocity;
        animator.SetFloat("Speed", rb.velocity.magnitude);

        if (moveInput.magnitude >= 0.1)
            spriteRenderer.flipX = Vector3.Dot(Vector3.right, moveInput) < 0;
    }

    public void ApplyDamage(float damage, Vector3 knockback)
    {
        if (IsDead)
            return;
        knockback.y = 0;
        StartCoroutine(HitRoutine(knockback * 2000));
        Health -= damage;
        sfx.Play();
        if ((Health <= 0) && (!IsDead))
        {
            IsDead = true;
            GameManager.Instance.GameOver();
        }
    }

    private IEnumerator HitRoutine(Vector3 knockback)
    {
        canDash = false;
        float timeLeft = 0f;
        float knockbackTime = 0.2f;
        spriteMat.SetFloat("_Hit", 1);
        while (timeLeft <= knockbackTime)
        {
            timeLeft += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            rb.AddForce(knockback * Time.fixedDeltaTime * (1 - timeLeft/ knockbackTime), ForceMode.VelocityChange);
            spriteMat.SetFloat("_Hit", (1 - timeLeft / knockbackTime));
            Sprite.transform.localScale = new Vector3(Sprite.transform.localScale.x,  Mathf.Lerp(10f, 8f, (1 - timeLeft / knockbackTime)), Sprite.transform.localScale.z);
        }
        canDash = true;
    }
}
