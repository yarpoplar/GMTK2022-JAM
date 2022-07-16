using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Parameters")]
	[SerializeField]
	private float fireSpeed;
	[SerializeField]
	private float bulletsPerShot;
	[SerializeField]
	private float spread;
	[SerializeField]
	private bool isAutomatic = false;
	[SerializeField]
	private GameObject bulletPrefab;

	private float cooldown = 0f;

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
		for (int i = 0; i < bulletsPerShot; i++)
		{
			Instantiate(bulletPrefab, transform.position, transform.rotation * Quaternion.Euler(0f, Random.Range(-spread, spread), 0f));
		}

		cooldown = 0f;
	}
}
