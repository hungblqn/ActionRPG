using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject statusUI;
    private bool isStatusUIOn = false;

    public TextMeshProUGUI textLevel,textEXP, textHP, textATK;
    private PlayerController playerController;

    private void Awake()
    {
        LockMouse();
    }
    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Update()
    {
        GetStatus();
        if (Input.GetKeyDown(KeyCode.I))
        {
            if(isStatusUIOn == false)
            {
                UnlockMouse();
                
                statusUI.SetActive(true);
                isStatusUIOn = true;
            }
            else
            {
                LockMouse();
                statusUI.SetActive(false);
                isStatusUIOn=false;
            }
        }
    }

    private void GetStatus()
    {
        if (playerController.GetLevel() >= 156)
        {
            textLevel.SetText("LEVEL: MAX");
        }
        else textLevel.SetText("LEVEL: " + playerController.GetLevel());
        textEXP.SetText("EXPERIENCE: " + playerController.GetExperience() + "/" +playerController.GetMaxExperience());
        textHP.SetText("HIT POINT: " + playerController.GetHP() + "/" + playerController.GetMaxHP());
        textATK.SetText("DAMAGE: " + playerController.GetDamage());
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
