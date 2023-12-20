using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    private PlayerController playerController;
    private GameObject player;
    private void Start()
    {
        SetMaxHP(GetHP());
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (GetHP() <= 0) Die();
        Assassinate();
        ThrowKunai();
    }
    private void Assassinate()
    {

    }
    private void ThrowKunai()
    {

    }
    private void Die()
    {
        playerController.GainExperience(GetExp());
        Destroy(gameObject);
        Debug.Log("Player gained " + GetExp() + " experience");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hit Area"))
        {
            StartCoroutine("GetHit");
            TakeDamage(playerController.GetDamage());
        }
        if (other.gameObject.CompareTag("Hit Sword"))
        {
            Debug.Log("Enemy take skill 2");
            TakeDamage(playerController.GetDamage());
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Fireball"))
        {
            Debug.Log("Enemy take skill 3");
            TakeDamage(playerController.GetDamage() * 10);
        }

    }
}
