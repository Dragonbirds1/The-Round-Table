using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    public List<PlayerInput> players = new List<PlayerInput>();

    public AssignPlayer assignPlayer;

    public ObjectFallController objectFallController;

    [Header("Teleport Spots")]
    public Transform[] teleportSpots; // Must be size 4

    [Header("Trigger")]
    public bool teleportNow = false;

    void Update()
    {
        if (!teleportNow)
            return;

        TeleportAllPlayers();

        teleportNow = false; // prevents infinite teleport loop
    }

    public void TeleportAllPlayers()
    {
        // Safety check
        if (teleportSpots.Length < players.Count)
        {
            Debug.LogError("Not enough teleport spots!");
            return;
        }

        for (int i = 0; i < players.Count; i++)
        {
            Transform playerTransform = players[i].transform;

            // ⭐ If using Rigidbody, move it safely
            Rigidbody rb = players[i].GetComponent<Rigidbody>();

            PlayerInput player = players[i];

            if (rb != null)
            {
                rb.position = teleportSpots[i].position;
                rb.linearVelocity = Vector3.zero; // stop leftover movement
                player.ActivateInput();
                objectFallController.StartFall = true;
            }
            else
            {
                playerTransform.position = teleportSpots[i].position;
                player.ActivateInput();
                objectFallController.StartFall = true;
            }
        }

        //Debug.Log("All players teleported!");
    }
}
