using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AutoDestroy");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
    }
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
