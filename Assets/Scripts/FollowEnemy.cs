using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    GameObject []enemy;
    private void Awake()
    {
        StartCoroutine("Destroy");
    }
    void Update()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemy.Length == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.LookAt(enemy[0].transform);
            transform.Translate(Vector3.forward * Time.deltaTime * 15f);
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
