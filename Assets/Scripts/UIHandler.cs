using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public Slider HPSlider;
    public TMP_Text WeaponName;
    public TMP_Text TaskName;

    private Player player;
    // Start is called before the first frame update
    private void Start()
    {
        player = GameManager.Instance.Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        HPSlider.value = player.Health / player.MaxHealth;
        if (GameManager.Instance.HaveWeapon)
            WeaponName.text = GameManager.Instance.playerWeapons[GameManager.Instance.currentWeapon].gameObject.name + ": " + GameManager.Instance.playerWeapons[GameManager.Instance.currentWeapon].ammo;
        if (GameManager.Instance.CurrentTask)
            TaskName.text = GameManager.Instance.CurrentTask.Name + ": " + GameManager.Instance.CurrentTask.Counter + GameManager.Instance.CurrentTask.PostCounter;
    }
}
