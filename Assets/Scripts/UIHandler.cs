using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Slider HPSlider;

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
    }
}
