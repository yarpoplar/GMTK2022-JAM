using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
	public static WeaponManager instance = null;

	[SerializeField]
	public List<Weapon> weapons = null;
	private int currentWeapon = 0;
	private float changeCooldown = 0f;
	public TMP_Text ammoCounter;

	private void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance == this)
		{
			Destroy(gameObject);
		}
	}

	public void Update()
	{
		changeCooldown += Time.deltaTime;

		if (changeCooldown >= 5f)
		{
			SwitchWeapon(Random.Range(0, weapons.Count - 1), 5);
			changeCooldown = 0f;
		}
	}

	public void SwitchWeapon(int index, int ammo)
	{
		weapons[currentWeapon].gameObject.SetActive(false);
		weapons[index].gameObject.SetActive(true);
		weapons[index].SetAmmo(ammo);
		currentWeapon = index;
		ammoCounter.text = ammo.ToString();
	}
}
