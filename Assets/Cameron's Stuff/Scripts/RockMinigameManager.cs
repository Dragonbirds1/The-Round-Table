using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockMinigameManager : MonoBehaviour
{
    public List<PlayerInput> players = new List<PlayerInput>();

    public List<PlayerInput> alivePlayers = new List<PlayerInput>();

    [Header("Teleport")]
    public Transform[] originalPositions;

    [Header("Wheel")]
    public Wheel wheel;

    public GameObject backgroundCh, groundCh, ChCam, NCam;

    //------------------------------------------------

    void Start()
    {
        // Copy players into alive list
        alivePlayers = new List<PlayerInput>(players);
    }

    //------------------------------------------------

    public void BeginMinigame()
    {
        alivePlayers = new List<PlayerInput>(players);

        foreach (var p in alivePlayers)
            p.gameObject.SetActive(true);
    }

    public void PlayerHit(PlayerInput player)
    {
        if (!alivePlayers.Contains(player))
            return;

        Debug.Log("Player " + player.playerIndex + " is OUT!");

        alivePlayers.Remove(player);

        //------------------------------------------------
        // Disable the player
        //------------------------------------------------

        player.gameObject.SetActive(false);

        //------------------------------------------------
        // Check winner
        //------------------------------------------------

        if (alivePlayers.Count == 1)
        {

            DeclareWinner(alivePlayers[0]);
        }
    }

    //------------------------------------------------

    void DeclareWinner(PlayerInput winner)
    {
        Debug.Log("PLAYER " + winner.playerIndex + " WINS!");

        //------------------------------------------------
        // Teleport everyone back
        //------------------------------------------------

        for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.SetActive(true);

            backgroundCh.SetActive(false);
            groundCh.SetActive(false);
            ChCam.SetActive(false);
            NCam.SetActive(true);

            players[i].transform.position =
                originalPositions[players[i].playerIndex].position;
        }

        //------------------------------------------------
        // Activate wheel for winner
        //------------------------------------------------

        MainGameManager.Instance.MinigameWinner(winner);

        Debug.Log("Winner can spin the wheel!");
    }
}
