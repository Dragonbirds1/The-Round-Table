using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectFallController : MonoBehaviour
{
    float wait = 0.1f;
    public GameObject fallingobject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Fall", wait, wait);
    }

    void Fall()
    {     
        Instantiate(fallingobject, new Vector3(Random.Range(-13, 13), 10, 0), Quaternion.identity);
    }
}
