using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
	[SerializeField]
	private Transform firePivot;

	private float cooldown = 0f;
	public int ammo = 5;

	private void OnDisable()
	{
		transform.localScale = new Vector3(.5f, .5f, .5f);
	}

	private void OnEnable()
	{
		transform.DOScale(new Vector3 (1f, 1f, 1f), .3f);
	}

	public void SetAmmo(int ammoAmount)
	{
		ammo = ammoAmount;
	}

	void Update()
	{
		// Mouse Look
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength = 100f;

		if (groundPlane.Raycast(ray, out rayLength))
		{
			Vector3 lookPos = ray.GetPoint(rayLength);
			firePivot.LookAt(new Vector3(lookPos.x, transform.position.y, lookPos.z));
		}


		if (cooldown < fireSpeed || ammo == 0)
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
			Instantiate(bulletPrefab, firePivot.position, firePivot.rotation * Quaternion.Euler(0f, Random.Range(-spread, spread), 0f));
		}

		ammo--;
		cooldown = 0f;

		WeaponManager.instance.ammoCounter.text = ammo.ToString();
	}
}
