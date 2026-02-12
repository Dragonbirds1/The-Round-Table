using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectFallController : MonoBehaviour
{
    float wait = .5f;
    public GameObject fallingobject;
    public float timer = 0;
    public GameObject barrier;
    public bool StartFall = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("Fall", wait, wait);
    }

    public void Update()
    {
        if (StartFall == true)
        {
            timer += Time.deltaTime;
            if (timer >= 10f)
            {
                StartFall = false;
                barrier.SetActive(false);
                timer = 0;
            }
        }
    }

    void Fall()
    {     
        Instantiate(fallingobject, new Vector3(Random.Range(19.5f, 44.4f), 9.5f, 0), Quaternion.identity);
    }
}
