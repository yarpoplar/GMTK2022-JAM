using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject Player;

    [Header("Dice")]
    [SerializeField]
    private float diceThrowCooldown = 30f;
    [SerializeField]
    private Transform diceSpawn;
    [SerializeField]
    private List<GameObject> dices;

    [Header("Weapon Manager")]
    [SerializeField]
    public List<Weapon> playerWeapons = null;
    public TMP_Text ammoCounter;

    public int currentWeapon = 0;
    public bool HaveWeapon = false;
    private float diceTime = 15f;

    [SerializeField]
    public TaskBase CurrentTask;

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

    private void Start()
    {
        ThrowDice(0, 0, 0);
    }

    private void Update()
	{
        //diceTime += Time.deltaTime;

        //if (diceTime >= diceThrowCooldown)
        //{
        //    ThrowDice(0, 0, 0);
        //    diceTime = 0f;
        //}
    }

    private void ThrowDice(int goodDices, int badDices, int chaoticDices)
	{
        foreach(GameObject dice in dices)
            Instantiate(dice, diceSpawn.position, Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)))).SetActive(true);
    }

    public void SwitchWeapon(int index)
    {
        HaveWeapon = true;
        int ammo = Random.Range(5, 10);

        playerWeapons[currentWeapon].gameObject.SetActive(false);
        playerWeapons[index].gameObject.SetActive(true);
        playerWeapons[index].SetAmmo(ammo);
        UpdateWeaponUI(ammo);

        currentWeapon = index;
    }

    public void UpdateWeaponUI(int ammo)
	{
        //ammoCounter.text = ammo.ToString();
    }

    public void NewTask(TaskBase task)
    {
        CurrentTask = Instantiate(task, gameObject.transform);
        CurrentTask.gameObject.SetActive(true);
    }

    public void TaskCompleted()
    {
        ThrowDice(0, 0, 0);
    }
}
