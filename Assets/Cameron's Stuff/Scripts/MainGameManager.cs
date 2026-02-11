using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance;

    //------------------------------------------------
    // MASTER PLAYER LIST
    //------------------------------------------------

    public List<PlayerInput> players = new List<PlayerInput>();

    //------------------------------------------------
    // REFERENCES
    //------------------------------------------------

    public Wheel wheel;
    public Teleport teleport;
    public Turns turns;
    public RockMinigameManager minigame;

    //------------------------------------------------
    // STATE MACHINE
    //------------------------------------------------

    public enum GameState
    {
        WaitingForPlayers,
        Turns,
        Event,
        Minigame,
        Wheel,
        Transition
    }

    public GameState CurrentState { get; private set; }

    //------------------------------------------------

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    //------------------------------------------------
    // STATE SWITCHER
    //------------------------------------------------

    public void ChangeState(GameState newState)
    {
        Debug.Log("STATE → " + newState);

        ExitState(CurrentState);

        CurrentState = newState;

        EnterState(newState);
    }

    //------------------------------------------------
    // ENTER STATES
    //------------------------------------------------

    void EnterState(GameState state)
    {
        switch (state)
        {
            case GameState.WaitingForPlayers:
                DisableAllInput();
                break;

            case GameState.Turns:
                EnableAllInput();
                turns.players = players;
                turns.BeginTurns();
                break;

            case GameState.Event:
                DisableAllInput();
                break;

            case GameState.Minigame:
                DisableAllInput();
                teleport.TeleportAllPlayers();
                minigame.players = players;
                minigame.BeginMinigame();
                break;

            case GameState.Wheel:
                EnableAllInput();
                break;

            case GameState.Transition:
                DisableAllInput();
                break;
        }
    }

    //------------------------------------------------
    // EXIT STATES (for future polish)
    //------------------------------------------------

    void ExitState(GameState state)
    {
        // Placeholder for future:
        // camera fades
        // music transitions
        // UI clears
    }

    //------------------------------------------------
    // PLAYER REGISTRATION
    //------------------------------------------------

    public void RegisterPlayer(PlayerInput player)
    {
        if (!players.Contains(player))
            players.Add(player);
    }

    //------------------------------------------------
    // MINIGAME WINNER FLOW
    //------------------------------------------------

    public void MinigameWinner(PlayerInput winner)
    {
        ChangeState(GameState.Transition);

        teleport.TeleportAllPlayers();

        ChangeState(GameState.Wheel);

        wheel.SpinForPlayer(winner);
    }

    //------------------------------------------------
    // INPUT CONTROL
    //------------------------------------------------

    void DisableAllInput()
    {
        foreach (var player in players)
            player.DeactivateInput();
    }

    void EnableAllInput()
    {
        foreach (var player in players)
        {
            player.ActivateInput();
            player.SwitchCurrentActionMap("Movement");
        }
    }
}
