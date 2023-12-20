using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    private Rigidbody rb;
    //attributes
    private int level=50;
    private float experience=0;
    private float damage=30;
    private float hp=100;

    private int str=0;
    private int agi=0;
    private int cha=0;
    private int luk=0;

    private float maxExperience = 1000;
    private float maxHP;

    private float movementSpeed=0.03f;

    //for jump
    public LayerMask groundMask;
    public bool grounded;
    public GameObject groundCheck;
    

    public GameObject attackArea1;
    public GameObject attackArea2;
    public GameObject attackArea3;
    private int attackIndex=1;

    //for skill
    private float skillCooldown1=1;
    private float skillCooldown2=1;
    private float skillCooldown3=1;
    private Image Cooldown1;
    private Image Cooldown2;
    private Image Cooldown3;
    private float timeStamp1=0;
    private float timeStamp2=0;
    private float timeStamp3=0;

    public GameObject skillPrefab2;
    public GameObject skillPrefab3;


    void Start()
    {
        Cooldown1 = GameObject.Find("Cooldown1").GetComponent<Image>();
        Cooldown2 = GameObject.Find("Cooldown2").GetComponent<Image>();
        Cooldown3 = GameObject.Find("Cooldown3").GetComponent<Image>();

        rb = GetComponent<Rigidbody>();
        maxHP = hp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Dodge();
        isGrounded();
        MeleeAttack();
        Skill();
        LevelUp();
    }
    void Skill()
    {
        Cooldown1.fillAmount = 1-((Time.time - timeStamp1) / skillCooldown1);
        Cooldown2.fillAmount = 1 - ((Time.time - timeStamp2) / skillCooldown2);
        Cooldown3.fillAmount = 1 - ((Time.time - timeStamp3) / skillCooldown3);
        //updating skill 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time-timeStamp1>=skillCooldown1 && level>=10)
        {
            Debug.Log("Cast skill 1");
            timeStamp1 = Time.time;
            

        }
        //completed skill 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && Time.time - timeStamp2 >= skillCooldown2 && level>=20)
        {
            Debug.Log("Cast skill 2");
            timeStamp2 = Time.time;
            Vector3 offset = transform.position + new Vector3(-.5f, 1f, 0);
            for (int i = 0; i < 5; i++)
            {
                Instantiate(skillPrefab2, transform.position+offset, transform.rotation);
                offset += new Vector3(0, 0, 5f);
            }
        }
        //updating...
        if (Input.GetKeyDown(KeyCode.Alpha3) && Time.time - timeStamp3 >= skillCooldown3 && level>=30)
        {
            Debug.Log("Cast skill 3");
            timeStamp3 = Time.time;
            //cast
            Instantiate(skillPrefab3, transform.position, transform.rotation);
        }
    }
    void LevelUp()
    {
        if (experience >= maxExperience)
        {
            experience = experience - maxExperience;
            level++;
            damage += (int)(damage / 10);
            maxHP += (int)(maxHP / 10);
            hp = maxHP;
            maxExperience += (int)(maxExperience / 10);
        }
    }
    //updating dodge....
    void Dodge()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            StartCoroutine("RollForward");
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            StartCoroutine("RollLeft");
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            StartCoroutine("RollBackward");
            
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            StartCoroutine("RollRight");
            
            

        }
    }
    IEnumerator RollForward()
    {
        rb.AddRelativeForce(Vector3.forward * 10, ForceMode.Impulse);
        this.anim.SetBool("dodge", true);
        yield return new WaitForSeconds(1.2f);
        this.anim.SetBool("dodge", false);
        rb.velocity = new Vector3(0, 0, 0);
    }
    IEnumerator RollLeft()
    {
        rb.AddRelativeForce(Vector3.left * 10, ForceMode.Impulse);
        this.anim.SetBool("dodge", true);
        yield return new WaitForSeconds(1.2f);
        this.anim.SetBool("dodge", false);
        rb.velocity = new Vector3(0, 0, 0);
    }
    IEnumerator RollBackward()
    {
        rb.AddRelativeForce(Vector3.back * 10, ForceMode.Impulse);
        this.anim.SetBool("dodge", true);
        yield return new WaitForSeconds(1.2f);
        this.anim.SetBool("dodge", false);
        rb.velocity = new Vector3(0, 0, 0);
    }
    IEnumerator RollRight()
    {
        rb.AddRelativeForce(Vector3.right * 10, ForceMode.Impulse);
        this.anim.SetBool("dodge", true);
        yield return new WaitForSeconds(1.2f);
        this.anim.SetBool("dodge", false);
        rb.velocity = new Vector3(0, 0, 0);
    }
    void MeleeAttack()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (attackIndex > 3) attackIndex = 1;
            Debug.Log("Attack " + attackIndex);
            StartCoroutine("Attack"+attackIndex);
            attackIndex++;
        }
    }
    IEnumerator Attack1()
    {
        attackArea2.SetActive(false);
        attackArea3.SetActive(false);

        this.anim.SetBool("melee1", true);
        attackArea1.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        this.anim.SetBool("melee1", false);
        attackArea1.SetActive(false);
    }
    IEnumerator Attack2()
    {
        attackArea1.SetActive(false);
        attackArea3.SetActive(false);

        this.anim.SetBool("melee2", true);
        attackArea2.SetActive(true);
        yield return new WaitForSeconds(1.1f);
        this.anim.SetBool("melee2", false);
        attackArea2.SetActive(false);
    }
    IEnumerator Attack3()
    {
        attackArea1.SetActive(false);
        attackArea2.SetActive(false);

        this.anim.SetBool("melee3", true);
        attackArea3.SetActive(true);
        yield return new WaitForSeconds(0.96f);
        this.anim.SetBool("melee3", false);
        attackArea3.SetActive(false);
    }
    void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 movement = this.transform.forward * verticalAxis + this.transform.right * horizontalAxis;
        movement.Normalize();

        this.transform.position += movement * movementSpeed;

        this.anim.SetFloat("vertical", verticalAxis);
        this.anim.SetFloat("horizontal", horizontalAxis);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.grounded)
        {
            this.rb.AddForce(Vector3.up * 4, ForceMode.Impulse);
        }
    }
    void isGrounded()
    {
        if(Physics.CheckSphere(groundCheck.transform.position, 0.2f, groundMask))
        {
            this.grounded = true;
        }
        else
        {
            this.grounded = false;
        }
        this.anim.SetBool("jump", !this.grounded);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = new Vector3(0, 0, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PHit Area"))
        {
            Debug.Log("Player take damage");
            TakeDamage(other.gameObject.GetComponentInParent<EnemyController>().GetDamage());
        }
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
    }
    public float GetDamage()
    {
        return damage;
    }
    public void GainExperience(float experience)
    {
        this.experience += experience;
    }
    public float GetExperience()
    {
        return experience;
    }
    public float GetMaxExperience()
    {
        return maxExperience;
    }
    public float GetLevel()
    {
        return level;
    }
    public float GetHP()
    {
        return hp;
    }
    public float GetMaxHP()
    {
        return maxHP;
    }
}
