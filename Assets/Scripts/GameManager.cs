using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject Player;

    [Header("Weapon Manager")]
    [SerializeField]
    public List<Weapon> playerWeapons = null;
    public TMP_Text ammoCounter;

    private int currentWeapon = 0;
    // TEMP FOR TESTING
    private float changeCooldown = 0f;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Player = players[0];
    }

	private void Update()
	{
        changeCooldown += Time.deltaTime;

        if (changeCooldown >= 5f)
        {
            SwitchWeapon(Random.Range(0, playerWeapons.Count - 1), 5);
            changeCooldown = 0f;
        }
    }

    public void SwitchWeapon(int index, int ammo)
    {
        playerWeapons[currentWeapon].gameObject.SetActive(false);
        playerWeapons[index].gameObject.SetActive(true);
        playerWeapons[index].SetAmmo(ammo);
        UpdateWeaponUI(ammo);

        currentWeapon = index;
    }

    public void UpdateWeaponUI(int ammo)
	{
        ammoCounter.text = ammo.ToString();
    }
}
