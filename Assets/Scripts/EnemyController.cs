using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject pHitArea1;

    public Animator anim;
    public GameObject floatingText;

    private float hp = 100;
    private float maxHP;
    private float exp = 100;
    private float damage = 20;
    private float movementSpeed = 2f;

    private PlayerController playerController;

    private GameObject player;

    public LayerMask playerMask;

    private float distance;

    private float attackCooldown = 2;
    private float attackCooldownTimeStamp;

    void Start()
    {
        attackCooldownTimeStamp = Time.time;
        maxHP = hp;

        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) distance = Vector3.Distance(transform.position, player.transform.position);
        if (hp <= 0) Die();
        if (distance < 10 && distance >= 1)
        {
            MoveToPlayer();
        }
        else if ( distance < 1 && canAttack())
        {
            AttackPlayer();
        }
    }
    private bool canAttack()
    {
        if (Time.time - attackCooldownTimeStamp > attackCooldown) return true;
        else return false;
    }
    private void AttackPlayer()
    {
        attackCooldownTimeStamp = Time.time;
        Debug.Log("Attack player!");
        StartCoroutine("Attack1");
    }
    IEnumerator Attack1()
    {
        anim.SetBool("attack", true);
        pHitArea1.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("attack", false);
        pHitArea1.SetActive(false);
    }
    private void MoveToPlayer()
    {
        transform.LookAt(player.transform.position);
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
    private void Die()
    {
        playerController.GainExperience(exp);
        Destroy(gameObject);
        Debug.Log("Player gained " + exp + " experience");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Hit Area"))
        {
            StartCoroutine("GetHit");
            TakeDamage(playerController.GetDamage());
            DisplaySwordDamage();
        }
        if(other.gameObject.CompareTag("Hit Sword"))
        {
            Debug.Log("Enemy take skill 2");
            TakeDamage(playerController.GetDamage());
            DisplaySwordDamage();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Fireball"))
        {
            Debug.Log("Enemy take skill 3");
            DisplayFireballDamage();
            TakeDamage(playerController.GetDamage() * 10);
        }
    
    }
    void DisplaySwordDamage()
    {
        GameObject display1Text = (GameObject)Instantiate(floatingText, transform.position + new Vector3(0, .5f, 0), transform.rotation);
        display1Text.GetComponent<FloatingText>().damage = playerController.GetDamage();
    }
    void DisplayFireballDamage()
    {
        GameObject displayText = (GameObject)Instantiate(floatingText, transform.position + new Vector3(0, .5f, 0), transform.rotation);
        displayText.GetComponent<FloatingText>().damage = playerController.GetDamage()*10;
    }
    IEnumerator GetHit()
    {
        this.anim.SetBool("gethit", true);
        yield return new WaitForSeconds(1);
        this.anim.SetBool("gethit", false);
    }

    private void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log(hp + "/" + maxHP);
    }

    public float GetDamage()
    {
        return damage;
    }
}
