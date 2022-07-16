using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField]
	private float fireSpeed;
	[SerializeField]
	private float cooldown = 0f;
	[SerializeField]
	private float bulletsPerShot;
	[SerializeField]
	private Vector2 spread;
	[SerializeField]
	private bool isAutomatic = false;
	[SerializeField]
	private GameObject bulletPrefab;


	void Update()
	{
		if (cooldown < fireSpeed)
		{
			cooldown += Time.deltaTime;
			return;
		}

		if (isAutomatic)
		{
			if (Input.GetMouseButton(0))
				Shoot();
		}
		else
			if (Input.GetMouseButtonDown(0))
			Shoot();
	}

	public void Shoot()
	{
		Instantiate(bulletPrefab, transform.position, transform.rotation);
		cooldown = 0f;
	}
}
