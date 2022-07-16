using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 15f;
    private Rigidbody rb = null;


    private Vector3 moveInput;
    private Vector3 moveVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        // Movement and look
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength = 100f;

        if (groundPlane.Raycast(ray, out rayLength))
		{
            Vector3 lookPos = ray.GetPoint(rayLength);
            transform.LookAt(new Vector3(lookPos.x, transform.position.y, lookPos.z));
		}
    }

	private void FixedUpdate()
	{
        rb.velocity = moveVelocity;
	}
}
