using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    public Slider sliderPlayerHP,sliderPlayerEXP;
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        GetStatus();
    }
    private void GetStatus()
    {
        sliderPlayerHP.value = playerController.GetHP() / playerController.GetMaxHP();
        sliderPlayerEXP.value = playerController.GetExperience() / playerController.GetMaxExperience();
    }
}
