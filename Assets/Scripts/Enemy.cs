using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float maxHP;
    [SerializeField] private float exp;
    [SerializeField] private float damage;
    [SerializeField] private float movementSpeed;
    public float GetHP()
    {
        return hp;
    }
    public void SetHP(float hp)
    {
        this.hp = hp;
    }
    public float GetMaxHP()
    {
        return maxHP;
    }
    public void SetMaxHP(float hp)
    {
        this.maxHP = hp;
    }
    public float GetExp()
    {
        return exp;
    }
    public void SetExp(float exp)
    {
        this.exp = exp;
    }
    public float GetDamage()
    {
        return damage;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public float GetMovementSpeed()
    {
        return movementSpeed;
    }
    public void SetMovementSpeed(float movementSpeed)
    {
        this.movementSpeed = movementSpeed;
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
    }


}
