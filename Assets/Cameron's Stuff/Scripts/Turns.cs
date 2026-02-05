using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.InputSystem.Controls;
using Unity.VisualScripting;

public class Turns : MonoBehaviour
{
    /// <summary>
    /// This script will manage the turns the the player can do and start the randomize for challenge and backstab event.
    /// </summary>

    [Header("Scripts")]
    public BackStabEvent backStabEvent;

    [Header("Lists")]
    public List<PlayerInput> players = new List<PlayerInput>();
    //public List<InputDevice> gamepads = new List<InputDevice>();

    [Header("Keycodes")]
    // V Keycodes go here V
    public KeyCode testKey;

    [Header("Bools")]
    public bool turn1;
    public bool turn2;
    public bool turn3;
    public bool turn4;
    public bool COS;

    private Gamepad controller1;
    private Gamepad controller2;
    private Gamepad controller3;
    private Gamepad controller4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var gamepads = Gamepad.all;
        int currentPlayers = players.Count;
        

        if (gamepads.Count == 4)
        {
            controller1 = gamepads[currentPlayers = 0];
            controller2 = gamepads[currentPlayers = 1];
            controller3 = gamepads[currentPlayers = 2];
            controller4 = gamepads[currentPlayers = 3];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (players.Count == 4)
        {
            turn1 = true;
        }

        if (turn1 == true)
        {
            if (controller1 != null)
            {
                if (controller1.buttonWest.wasPressedThisFrame)
                {
                    Debug.Log("Player1 turn taken, it's player2's turn now.");
                    turn1 = false;
                    turn2 = true;
                }
            }
        }
        if (turn2 == true)
        {
            if (controller2 != null)
            {
                if (controller2.buttonWest.wasPressedThisFrame)
                {
                    Debug.Log("Player2 turn taken, it's player3's turn now.");
                    turn2 = false;
                    turn3 = true;
                }
            }
        }
        if (turn3 == true)
        {
            if (controller3 != null)
            {
                if (controller3.buttonWest.wasPressedThisFrame)
                {
                    Debug.Log("Player3 turn taken, it's player3's turn now.");
                    turn3 = false;
                    turn4 = true;
                }
            }
        }
        if (turn4 == true)
        {
            if (controller4 != null)
            {
                if (controller4.buttonWest.wasPressedThisFrame)
                {
                    Debug.Log("Player4 turn taken, time to see if a challenge starts or if a backstab starts.");
                    backStabEvent.start = true;
                }
            }
        }
    }
}
