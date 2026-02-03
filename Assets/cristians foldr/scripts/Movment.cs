using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class Movment : MonoBehaviour
{
    
    public float speed = 20f;
    private Rigidbody2D rb;
    private Vector2 input;

    public Text WINTEXT;
   

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Win")
        {
            WINTEXT.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }


}
