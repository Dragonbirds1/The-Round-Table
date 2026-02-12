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

    [Header("Turns")]
    public Turns turns;

    [Header("RockFall")]
    public ObjectFallController fall;

    public GameObject backgroundCh, groundCh, ChCam, NCam, abc1, abc2;

    private bool winnerDeclared = false;

    //------------------------------------------------

    void Start()
    {
        // Copy players into alive list
        alivePlayers = new List<PlayerInput>(players);
        winnerDeclared = false;
    }

    //------------------------------------------------

    public void BeginMinigame()
    {
        players = MainGameManager.Instance.players;

        alivePlayers = new List<PlayerInput>(players);

        Debug.Log("MINIGAME STARTED WITH " + alivePlayers.Count + " PLAYERS");
        Debug.Log(players.Count);

        foreach (var p in alivePlayers)
            p.gameObject.SetActive(true);
    }

    public void PlayerHit(PlayerInput player)
    {
        if (winnerDeclared)
            return;

        if (!alivePlayers.Contains(player))
            return;

        Debug.Log("Player " + player.playerIndex + " is OUT!");

        alivePlayers.Remove(player);
        EliminatePlayer(player);

        if (alivePlayers.Count <= 1)
        {
            winnerDeclared = true;

            if (alivePlayers.Count == 1)
                DeclareWinner(alivePlayers[0]);
            else
                Debug.Log("Nobody survived!");
        }
    }

    void EliminatePlayer(PlayerInput player)
    {
        player.DeactivateInput();

        player.GetComponent<Collider2D>().enabled = false;

        // Optional visual
        player.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    //------------------------------------------------

    void DeclareWinner(PlayerInput winner)
    {
        foreach (var p in players)
        {
            Debug.Log("Player seat loop index: " + players.IndexOf(p));
            Debug.Log("Actual PlayerIndex: " + p.playerIndex);
        }

        Debug.Log("PLAYER " + winner.playerIndex + " WINS!");

        //------------------------------------------------
        // Teleport everyone back
        //------------------------------------------------

        backgroundCh.SetActive(false);
        groundCh.SetActive(false);
        ChCam.SetActive(false);
        NCam.SetActive(true);
        fall.barrier.SetActive(true);
        abc1.SetActive(true);
        abc2.SetActive(true);

        for (int i = 0; i < players.Count; i++)
        {
            if (i >= originalPositions.Length)
            {
                Debug.LogError("Missing spawn point for seat " + i);
                return;
            }

            PlayerInput p = players[i];

            Rigidbody2D rb = p.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }

            p.DeactivateInput();
            p.GetComponent<Collider2D>().enabled = true;
            p.GetComponent<SpriteRenderer>().color = Color.white;
            p.transform.position =
                originalPositions[i].position;
        }

        //------------------------------------------------
        // Activate wheel for winner
        //------------------------------------------------

        MainGameManager.Instance.MinigameWinner(winner);

        Debug.Log("Winner can spin the wheel!");
    }
}
