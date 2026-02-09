using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turns : MonoBehaviour
{
    public BackStabEvent backStabEvent;

    public List<PlayerInput> players = new List<PlayerInput>();

    private int currentTurn = 0;

    private int eventChance;

    public GameObject itemBar;

    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI gogglesDisplay;

    public bool turnSwitch, item;

    public string player1, player2, player3, player4;

    private void Start()
    {
        itemBar.SetActive(false);
        turnSwitch = false;
        item = false;
    }
    void Update()
    {
        if (players.Count == 0) return;

        PlayerInput activePlayer = players[currentTurn];

        if (currentTurn == 0)
        {
            currentPlayerText.text = player1;
        }
        else if (currentTurn == 1)
        {
            currentPlayerText.text = player2;
         
        }
        else if (currentTurn == 2)
        {
            currentPlayerText.text = player3;
        }
        else if (currentTurn == 3)
        {
            currentPlayerText.text = player4;
        }

        if (Pressed(activePlayer) && turnSwitch == true && backStabEvent.stabbing == false)
        {
            turnSwitch = false;

            Debug.Log("Player " + activePlayer.playerIndex + " took their turn!");

            StartCoroutine(TurnDelay());
        }

    }

    public PlayerInput GetCurrentPlayer()
    {
        if (players == null || players.Count == 0)
            return null;

        return players[currentTurn];
    }

    IEnumerator TurnDelay()
    {
        yield return new WaitForSeconds(0.25f);
        NextTurn();
    }

    bool Pressed(PlayerInput player)
    {
        var device = player.devices[0];

        if (device is Gamepad pad)
            if (pad.buttonWest.wasPressedThisFrame && item == false)
            {
                turnSwitch = true;
                return pad.buttonWest.wasPressedThisFrame;
            }

            else if (pad.buttonEast.wasPressedThisFrame && item == false)
            {
                itemBar.SetActive(true);
                turnSwitch = false;
                item = true;
            }
            else if (pad.buttonWest.wasPressedThisFrame && item == true)
            {
                Debug.Log("Used SwitchBlade!");
                itemBar.SetActive(false);
                turnSwitch = true;
                item = false;
                return pad.buttonWest.wasPressedThisFrame;
            }
            else if (pad.buttonSouth.wasPressedThisFrame && item == true)
            {
                Debug.Log("Used Vial Of Lavander!");
                itemBar.SetActive(false);
                turnSwitch = true;
                item = false;
                return pad.buttonSouth.wasPressedThisFrame;
            }
            else if (pad.buttonEast.wasPressedThisFrame && item == true)
            {
                Debug.Log("Used Goggles!");
                gogglesDisplay.text = "Player: " + backStabEvent.winner;
                itemBar.SetActive(false);
                turnSwitch = true;
                item = false;
                return pad.buttonEast.wasPressedThisFrame;
            }


        if (device is Keyboard keyboard)
                return keyboard.spaceKey.wasPressedThisFrame;

        return false;
    }

    void NextTurn()
    {
        currentTurn++;

        if (currentTurn >= players.Count)
        {
            Debug.Log("Round finished → Challenge or Backstab!");
            eventChance = Random.Range(0, 10);
            if (eventChance <= 2)
            {
                Debug.Log("Backstab check!");
                backStabEvent.start = true;
                currentTurn = 0; // reset for next round
            }
            else if (eventChance > 2 && eventChance <= 10)
            {
                Debug.Log("Challenge check!");
                currentTurn = 0; // reset for next round
            }
        }
    }
}
