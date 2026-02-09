using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turns : MonoBehaviour
{
    public BackStabEvent backStabEvent;

    public List<PlayerInput> players = new List<PlayerInput>();

    private int currentTurn = 0;

    private int eventChance;

    void Update()
    {
        if (players.Count == 0) return;

        PlayerInput activePlayer = players[currentTurn];

        if (Pressed(activePlayer)  && backStabEvent.stabbing == false)
        {
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
            return pad.buttonWest.wasPressedThisFrame;

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
