using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Wheel : MonoBehaviour
{
    /// <summary>
    /// This script will handle the wheel's spin and results.
    /// </summary>

    [Header("Wheel Settings")]
    public Transform spinPart;
    public KeyCode spinKey;
    bool currentlySpinning = false;
    public Vector2 velocity;
    public float spinAcceleration;
    public float wheelRadius;
    public float friction;
    public float X;
    public float Y;
    public float radToSpinWheel;
    bool startSpinning = false;
    public bool canSpin = false;
    public KeyCode spinPlayer1, spinPlayer2, spinPlayer3, spinPlayer4;
    public AssignPlayer assignPlayer;
    public int controllersConnected;

    private float currentSpinSpeed;

    [Header ("Scripts")]
    public WinnerChoice winnerChoice;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(spinPlayer1))
        {
            Debug.Log("Player1");
            return;
        }
        if (Input.GetButton("Spin2"))
        {
            Debug.Log("Player2");
            return;
        }
        if (Input.GetKey(spinPlayer3))
        {
            Debug.Log("Player3");
            return;
        }
        if (Input.GetKey(spinPlayer4))
        {
            Debug.Log("Player4");
            return;
        }
        if (canSpin == true)
        {
            if (startSpinning == true)
            {
                X += Time.deltaTime;
                if (X >= 5)
                {
                    currentlySpinning = true;
                    startSpinning = false;
                }
            }
            spinPart.Rotate(0f, 0f, X);
            if (Input.GetKeyDown(spinKey) && currentlySpinning == false)
            {
                startSpinning = true;
            }
            if (currentlySpinning == true)
            {
                if (X > 0f)
                {
                    X -= Time.deltaTime;
                }
                else if (X <= 0f)
                {
                    X = 0f;
                }
            }
        }
    }

    public void AddControler()
    {
        controllersConnected += 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, wheelRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, radToSpinWheel);
    }
}
