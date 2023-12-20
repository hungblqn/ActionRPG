using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private GameObject player;
    private GameObject camera;
    public float damage=0;
    private void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Camera");
        GetComponent<TextMeshPro>().SetText("-" + damage);
        transform.LookAt(camera.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
        StartCoroutine("Destroy");
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
