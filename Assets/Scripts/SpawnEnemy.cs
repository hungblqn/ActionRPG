using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length ==0)
        {
            Spawn();
        }
    }
    void Spawn()
    {
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
