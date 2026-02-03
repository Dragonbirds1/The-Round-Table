using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class WinnerChoice : MonoBehaviour
{
    /// <summary>
    /// This script will decide who the winner is based on the first player to finish.
    /// </summary>

    [Header("Settings")]
    public int Choice;
    public KeyCode choiceKey;
    public bool player1Winner, player2Winner, player3Winner, player4Winner;

    private int choiceIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1Winner = false;
        player2Winner = false;
        player3Winner = false;
        player4Winner = false;
    }

    // Update is called once per frame
    void Update()
    {
        Choice = choiceIndex;

        if (Input.GetKeyDown(choiceKey)) // Just for testing purposes, REMOVE WHEN DONE!!!
        {
            choiceIndex = Random.Range(1, 4); // Just for testing purposes, REMOVE WHEN DONE!!!

            if (Choice == 1)
            {
                Debug.Log("Player 1 is the winner!");
                player1Winner = true;
                return;
            }
            else if (Choice == 2)
            {
                Debug.Log("Player 2 is the winner!");
                player2Winner = true;
                return;
            }
            else if (Choice == 3)
            {
                Debug.Log("Player 3 is the winner!");
                player3Winner = true;
                return;
            }
            if (Choice == 4)
            {
                Debug.Log("Player 4 is the winner!");
                player4Winner = true;
                return;
            }
        }
    }
}
