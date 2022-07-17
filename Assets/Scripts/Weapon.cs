using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class Weapon : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField]
	private GameObject bulletPrefab;
	[SerializeField]
	private ParticleSystem muzzleVFX;
	[SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
	private Transform firePivot;
	[SerializeField]
	private bool isEnemy = false;
	[Space]
	[Header("Parameters")]
	[SerializeField]
	public float fireSpeed;
	[SerializeField]
	private float bulletsPerShot;
	[SerializeField]
	private float spread;
	[SerializeField]
	private Vector3 additionalForce = new Vector3(0f,0f,0f);
	[SerializeField]
	private float shake = 1f;
	[SerializeField]
	private bool isAutomatic = false;
	[SerializeField]
	private Vector3 recoilVector = new Vector3(1f, 0f, 0.2f);

	private float cooldown = 0f;
	public int baseAmmo = 5;
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
		ammo = (int)Mathf.Round((ammoAmount * .1f) * baseAmmo);
	}

	void Update()
	{
		if (spriteRenderer)
			spriteRenderer.flipY = Vector3.Dot(Vector3.right, transform.forward) < 0;

        if (isEnemy)
        {
            if (cooldown < fireSpeed || ammo == 0)
                cooldown += Time.deltaTime;
			return;
		}

		// Mouse Look
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
		float rayLength = 100f;

		if (groundPlane.Raycast(ray, out rayLength))
		{
			Vector3 lookPos = ray.GetPoint(rayLength);
			transform.LookAt(new Vector3(lookPos.x, transform.position.y, lookPos.z));
		}

        if (cooldown < fireSpeed || ammo == 0)
        {
            cooldown += Time.deltaTime;
		}

		if (GameManager.Instance.Player.GetComponent<Player>().IsDead)
			return;

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
        if (cooldown < fireSpeed || ammo == 0)
            return;

        for (int i = 0; i < bulletsPerShot; i++)
		{
			var bullet = Instantiate(bulletPrefab, firePivot.position, firePivot.rotation * Quaternion.Euler(Random.Range(-spread, spread), 0f, 0f));
			if (additionalForce != Vector3.zero)
				bullet.GetComponent<Rigidbody>().AddForce(additionalForce, ForceMode.Impulse);
		}

		WeaponFeedback();
		
		cooldown = 0f;
        if (!isEnemy)
        {
            ammo--;
            //GameManager.Instance.ammoCounter.text = ammo.ToString();
        }
	}

	public void WeaponFeedback()
	{
		// Shake
		Camera.main.GetComponent<CinemachineImpulseSource>().GenerateImpulse(shake);
		// Recoil
		transform.DOPunchPosition(recoilVector, cooldown / 2);
		// Muzzle
		if (muzzleVFX == null)
			return;

		muzzleVFX.Play();
		ParticleSystem.EmissionModule em = muzzleVFX.emission;
		em.enabled = true;
	}
}
