using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float moveSpeed = 15f;
    [SerializeField]
    private float dashCooldown = 1f;
    [SerializeField]
    private float dashTime = .2f;
    [SerializeField]
    private float dashVelocity = 1200f;
    private Rigidbody rb = null;
    [SerializeField]
    private GameObject gfx;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private bool isDashing = false;
    private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (isDashing)
            return;

        // Movement
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector3.zero && canDash)
            StartCoroutine(DashRoutine(moveInput));
	}

    private IEnumerator DashRoutine(Vector3 direction)
	{
        isDashing = true;
        canDash = false;
        float timeLeft = 0f;

        gfx.transform.DOScale(new Vector3(.5f, .5f, .5f), dashTime / 2);

        while (timeLeft <= dashTime)
		{
            timeLeft += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
            rb.AddForce(direction * dashVelocity * Time.fixedDeltaTime, ForceMode.VelocityChange);

            if (Mathf.Round(timeLeft) == Mathf.Round(dashTime / 2))
                gfx.transform.DOScale(new Vector3(1, 1, 1), dashTime / 2);
        }

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown - dashTime);

        canDash = true;

    }

	private void FixedUpdate()
	{
        rb.velocity = moveVelocity;
    }
}
