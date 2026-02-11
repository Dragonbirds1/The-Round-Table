using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Turns : MonoBehaviour
{
    public BackStabEvent backStabEvent;

    public Teleport teleport;

    public List<PlayerInput> players = new List<PlayerInput>();

    private HashSet<int> skippedPlayers = new HashSet<int>();

    private Dictionary<int, int> seatToPlayer = new Dictionary<int, int>();

    private int currentTurn = 0;

    private int eventChance;

    private int itemChance;

    private int typeOfItem;

    public int luckyPlayer;

    public int currentSBP1, currentVOLP1, currentGP1;
    public int currentSBP2, currentVOLP2, currentGP2;
    public int currentSBP3, currentVOLP3, currentGP3;
    public int currentSBP4, currentVOLP4, currentGP4;

    public Animator switchBlade;

    public Animator loadingAnim;

    public GameObject itemBar;

    public TextMeshProUGUI currentPlayerText;
    public TextMeshProUGUI gogglesDisplay;
    public TextMeshProUGUI itemBarSB1, itemBarSB2, itemBarSB3, itemBarSB4;
    public TextMeshProUGUI itemBarVOL1, itemBarVOL2, itemBarVOL3, itemBarVOL4;
    public TextMeshProUGUI itemBarG1, itemBarG2, itemBarG3, itemBarG4;
    public TextMeshProUGUI latestLuckyPlayer;

    private PlayerInput lucky;

    public GameObject light1, light2, light3, backgroundCh, groundCh, ChCam, NCam;

    public bool turnSwitch, item, ready, skipTurn, sb1, sb2, sb3, sb4, vol1, vol2, vol3, vol4, g1, g2, g3, g4, toggleSwap;

    public float stabReady = 0.096f;

    public float startSwap = 2f;

    public string player1, player2, player3, player4, view1, view2, view3, view4;

    private bool choosingSkipTarget = false;
    private int skipTargetIndex = 0;
    // Seats: 0=Top(Y), 1=Right(B), 2=Bottom(A), 3=Left(X)
    private int[] seatOrder = new int[4];


    private void Start()
    {
        itemBar.SetActive(false);
        turnSwitch = false;
        item = false;
        skipTurn = false;
        toggleSwap = false;

        for (int i = 0; i < players.Count; i++)
        {
            seatToPlayer[i] = i;
        }

    }
    void Update()
    {
        if (players.Count == 0) return;

        PlayerInput activePlayer = players[currentTurn];

        itemBarSB1.text = currentSBP1.ToString();
        itemBarSB2.text = currentSBP2.ToString();
        itemBarSB3.text = currentSBP3.ToString();
        itemBarSB4.text = currentSBP4.ToString();
        
        itemBarVOL1.text = currentVOLP1.ToString();
        itemBarVOL2.text = currentVOLP2.ToString();
        itemBarVOL3.text = currentVOLP3.ToString();
        itemBarVOL4.text = currentVOLP4.ToString();

        itemBarG1.text = currentGP1.ToString();
        itemBarG2.text = currentGP2.ToString();
        itemBarG3.text = currentGP3.ToString();
        itemBarG4.text = currentGP4.ToString();

        if (toggleSwap == true)
        {
            startSwap -= Time.deltaTime;
            if (startSwap <= 0)
            {
                loadingAnim.SetBool("Swap", false);
                startSwap = 2f;
                toggleSwap = false;
            }
            else if (startSwap <= 1)
            {
                groundCh.SetActive(true);
                backgroundCh.SetActive(true);
                ChCam.SetActive(true);
                NCam.SetActive(false);
                teleport.teleportNow = true;
            }
        }

        if (choosingSkipTarget)
        {
            HandleSkipSelection(activePlayer);
            return;
        }

        if (currentTurn == 0)
        {
            itemBarSB1.enabled = true;
            itemBarSB2.enabled = false;
            itemBarSB3.enabled = false;
            itemBarSB4.enabled = false;
            itemBarVOL1.enabled = true;
            itemBarVOL2.enabled = false;
            itemBarVOL3.enabled = false;
            itemBarVOL4.enabled = false;
            itemBarG1.enabled = true;
            itemBarG2.enabled = false;
            itemBarG3.enabled = false;
            itemBarG4.enabled = false;

            currentPlayerText.text = player1;
            if (currentSBP1 > 0)
            {
                sb1 = true;
            }
            else if (currentSBP1 <= 0)
            {
                sb1 = false;
            }

            if (currentVOLP1 > 0)
            {
                vol1 = true;
            }
            else if (currentVOLP1 <= 0)
            {
                vol1 = false;
            }

            if (currentGP1 > 0)
            {
                g1 = true;
            }
            else if (currentGP1 <= 0)
            {
                g1 = false;
            }
        }
        else if (currentTurn == 1)
        {
            itemBarSB1.enabled = false;
            itemBarSB2.enabled = true;
            itemBarSB3.enabled = false;
            itemBarSB4.enabled = false;
            itemBarVOL1.enabled = false;
            itemBarVOL2.enabled = true;
            itemBarVOL3.enabled = false;
            itemBarVOL4.enabled = false;
            itemBarG1.enabled = false;
            itemBarG2.enabled = true;
            itemBarG3.enabled = false;
            itemBarG4.enabled = false;

            currentPlayerText.text = player2;
            if (currentSBP2 > 0)
            {
                sb2 = true;
            }
            else if (currentSBP2 <= 0)
            {
                sb2 = false;
            }

            if (currentVOLP2 > 0)
            {
                vol2 = true;
            }
            else if (currentVOLP2 <= 0)
            {
                vol2 = false;
            }

            if (currentGP2 > 0)
            {
                g2 = true;
            }
            else if (currentGP2 <= 0)
            {
                g2 = false;
            }
        }
        else if (currentTurn == 2)
        {
            itemBarSB1.enabled = false;
            itemBarSB2.enabled = false;
            itemBarSB3.enabled = true;
            itemBarSB4.enabled = false;
            itemBarVOL1.enabled = false;
            itemBarVOL2.enabled = false;
            itemBarVOL3.enabled = true;
            itemBarVOL4.enabled = false;
            itemBarG1.enabled = false;
            itemBarG2.enabled = false;
            itemBarG3.enabled = true;
            itemBarG4.enabled = false;

            currentPlayerText.text = player3;
            if (currentSBP3 > 0)
            {
                sb3 = true;
            }
            else if (currentSBP3 <= 0)
            {
                sb3 = false;
            }

            if (currentVOLP3 > 0)
            {
                vol3 = true;
            }
            else if (currentVOLP3 <= 0)
            {
                vol3 = false;
            }

            if (currentGP3 > 0)
            {
                g3 = true;
            }
            else if (currentGP3 <= 0)
            {
                g3 = false;
            }
        }
        else if (currentTurn == 3)
        {
            itemBarSB1.enabled = false;
            itemBarSB2.enabled = false;
            itemBarSB3.enabled = false;
            itemBarSB4.enabled = true;
            itemBarVOL1.enabled = false;
            itemBarVOL2.enabled = false;
            itemBarVOL3.enabled = false;
            itemBarVOL4.enabled = true;
            itemBarG1.enabled = false;
            itemBarG2.enabled = false;
            itemBarG3.enabled = false;
            itemBarG4.enabled = true;

            currentPlayerText.text = player4;
            if (currentSBP4 > 0)
            {
                sb4 = true;
            }
            else if (currentSBP4 <= 0)
            {
                sb4 = false;
            }

            if (currentVOLP4 > 0)
            {
                vol4 = true;
            }
            else if (currentVOLP4 <= 0)
            {
                vol4 = false;
            }

            if (currentGP4 > 0)
            {
                g4 = true;
            }
            else if (currentGP4 <= 0)
            {
                g4 = false;
            }
        }

        if (Pressed(activePlayer) && turnSwitch == true && backStabEvent.stabbing == false)
        {
            turnSwitch = false;

            Debug.Log("Player " + activePlayer.playerIndex + " took their turn!");

            StartCoroutine(TurnDelay());
        }

        if (ready == true) 
        { 
            stabReady -= Time.deltaTime;
            if (stabReady <= 0f)
            {
                backStabEvent.winner = activePlayer.playerIndex;
                itemBar.SetActive(false);
                backStabEvent.start = true;
                stabReady = 0.096f;
                ready = false;
                turnSwitch = true;
                currentTurn++;
                BuildSeatOrder();
                if (currentTurn >= players.Count)
                {
                    currentTurn = 0;
                }
            }
        }

    }

    void BuildSeatOrder()
    {
        for (int i = 0; i < players.Count && i < 4; i++)
        {
            seatOrder[i] = i;
        }
    }

    public void BeginTurns()
    {
        Debug.Log("Turns Started");

        currentTurn = 0;

        turnSwitch = true;

        // Optional: reset UI here
    }

    void HandleSkipSelection(PlayerInput activePlayer)
    {
        if (activePlayer.devices.Count == 0)
            return;

        if (!(activePlayer.devices[0] is Gamepad pad))
            return;

        int targetSeat = -1;

        // Button → seat mapping
        if (pad.buttonNorth.wasPressedThisFrame) targetSeat = 0; // Y (Top)
        if (pad.buttonEast.wasPressedThisFrame) targetSeat = 1; // B (Right)
        if (pad.buttonSouth.wasPressedThisFrame) targetSeat = 2; // A (Bottom)
        if (pad.buttonWest.wasPressedThisFrame) targetSeat = 3; // X (Left)

        if (targetSeat == -1)
            return;

        // Make sure seat exists
        if (targetSeat >= players.Count)
            return;

        PlayerInput targetPlayer = players[targetSeat];

        // 🚫 Prevent skipping yourself (CORRECT check)
        if (targetPlayer == activePlayer)
        {
            Debug.Log("Cannot skip yourself");
            return;
        }

        Debug.Log("Lavender used! Skipping Player " + targetPlayer.playerIndex);

        skippedPlayers.Add(players.IndexOf(targetPlayer));

        choosingSkipTarget = false;
        turnSwitch = true;

        StartCoroutine(TurnDelay());
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
            if (pad.buttonWest.wasPressedThisFrame && item == false && backStabEvent.stabbing == false)
            {
                light1.SetActive(false);
                light2.SetActive(false);
                light3.SetActive(false);
                loadingAnim.SetBool("Swap", true);
                toggleSwap = true;
                turnSwitch = true;
                return pad.buttonWest.wasPressedThisFrame;
            }

            else if (pad.buttonEast.wasPressedThisFrame && item == false && backStabEvent.stabbing == false)
            {
                itemBar.SetActive(true);
                turnSwitch = false;
                item = true;
            }
            else if (pad.buttonWest.wasPressedThisFrame && item == true && backStabEvent.stabbing == false)
            {
                if (sb1 == true || sb2 == true || sb3 == true || sb4 == true)
                {
                    Debug.Log("Used SwitchBlade!");
                    switchBlade.SetBool("Stab", true);
                    item = false;
                    ready = true;
                    if (currentTurn == 0)
                    {
                        currentSBP1--;
                    }
                    else if (currentTurn == 1)
                    {
                        currentSBP2--;
                    }
                    else if (currentTurn == 2)
                    {
                        currentSBP3--;
                    }
                    else if (currentTurn == 3)
                    {
                        currentSBP4--;
                    }
                    vol1 = false;
                    vol2 = false;
                    vol3 = false;
                    vol4 = false;
                }
                else if (sb1 == false || sb2 == false || sb3 == false || sb4 == false)
                {
                    Debug.Log("Not enough SB, GO GET MORE!!!");
                    item = false;
                    itemBar.SetActive(false);
                    turnSwitch = true;
                }
            }
            else if (pad.buttonSouth.wasPressedThisFrame && item == true && backStabEvent.stabbing == false)
            {
                if (vol1 == true || vol2 == true || vol3 == true || vol4 == true)
                {
                    Debug.Log("Used Vial Of Lavender!");

                    choosingSkipTarget = true;
                    skipTargetIndex = (currentTurn + 1) % players.Count; // default target

                    itemBar.SetActive(false);
                    turnSwitch = false;
                    item = false;
                    if (currentTurn == 0)
                    {
                        currentVOLP1--;
                    }
                    else if (currentTurn == 1)
                    {
                        currentVOLP2--;
                    }
                    else if (currentTurn == 2)
                    {
                        currentVOLP3--;
                    }
                    else if (currentTurn == 3)
                    {
                        currentVOLP4--;
                    }
                    sb1 = false;
                    sb2 = false;
                    sb3 = false;
                    sb4 = false;
                }
                else if (vol1 == false || vol2 == false || vol3 == false || vol4 == false)
                {
                    Debug.Log("Not enough VOL, GO GET MORE!!!");
                    item = false;
                    itemBar.SetActive(false);
                    turnSwitch = true;
                }
            }
            else if (pad.buttonEast.wasPressedThisFrame && item == true && backStabEvent.stabbing == false)
            {
                if (g1 == true || g2 == true || g3 == true || g4 == true)
                {
                    Debug.Log("Used Goggles!");
                    if (backStabEvent.winner == 0)
                    {
                        gogglesDisplay.text = "Stabber will be: " + view1;
                    }
                    else if (backStabEvent.winner == 1)
                    {
                        gogglesDisplay.text = "Stabber will be: " + view2;
                    }
                    else if (backStabEvent.winner == 2)
                    {
                        gogglesDisplay.text = "Stabber will be: " + view3;
                    }
                    else if (backStabEvent.winner == 3)
                    {
                        gogglesDisplay.text = "Stabber will be: " + view4;
                    }
                    itemBar.SetActive(false);
                    turnSwitch = true;
                    item = false;
                    if (currentTurn == 0)
                    {
                        currentGP1--;
                    }
                    else if (currentTurn == 1)
                    {
                        currentGP2--;
                    }
                    else if (currentTurn == 2)
                    {
                        currentGP3--;
                    }
                    else if (currentTurn == 3)
                    {
                        currentGP4--;
                    }
                    g1 = false;
                    g2 = false;
                    g3 = false;
                    g4 = false;
                    return pad.buttonEast.wasPressedThisFrame;
                }
                else if (g1 == false || g2 == false || g3 == false || g4 == false)
                {
                    Debug.Log("Not enough G, GO GET MORE!!!");
                    item = false;
                    itemBar.SetActive(false);
                    turnSwitch = true;
                }
            }


        if (device is Keyboard keyboard)
                return keyboard.spaceKey.wasPressedThisFrame;

        return false;
    }

    void NextTurn()
    {
        do
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
                }
                else
                {
                    Debug.Log("Challenge check!");
                    itemChance = Random.Range(0, 10);
                }

                if (itemChance <= 4)
                {
                    typeOfItem = Random.Range(0, 10);
                    luckyPlayer = Random.Range(0, players.Count);
                    lucky = players[luckyPlayer];
                    int luckyIndex = players.IndexOf(lucky);
                    if (luckyIndex <= 0)
                    {
                        if (typeOfItem <= 2)
                        {
                            currentSBP1++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a SwitchBlade.";
                        }
                        else if (typeOfItem <= 5 && typeOfItem > 2)
                        {
                            currentVOLP1++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a Vile of Lavander.";
                        }
                        else if (typeOfItem <= 10 && typeOfItem > 5)
                        {
                            currentGP1++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a pair of Goggles.";
                        }
                    }
                    else if (luckyIndex == 1)
                    {
                        if (typeOfItem <= 2)
                        {
                            currentSBP2++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a SwitchBlade.";
                        }
                        else if (typeOfItem <= 5 && typeOfItem > 2)
                        {
                            currentVOLP2++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a Vile of Lavander.";
                        }
                        else if (typeOfItem <= 10 && typeOfItem > 5)
                        {
                            currentGP2++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a pair of Goggles.";
                        }
                    }
                    else if (luckyIndex == 2)
                    {
                        if (typeOfItem <= 2)
                        {
                            currentSBP3++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a SwitchBlade.";
                        }
                        else if (typeOfItem <= 5 && typeOfItem > 2)
                        {
                            currentVOLP3++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a Vile of Lavander.";
                        }
                        else if (typeOfItem <= 10 && typeOfItem > 5)
                        {
                            currentGP3++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a pair of Goggles.";
                        }
                    }
                    else if (luckyIndex == 3)
                    {
                        if (typeOfItem <= 2)
                        {
                            currentSBP4++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a SwitchBlade.";
                        }
                        else if (typeOfItem <= 5 && typeOfItem > 2)
                        {
                            currentVOLP4++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a Vile of Lavander.";
                        }
                        else if (typeOfItem <= 10 && typeOfItem > 5)
                        {
                            currentGP4++;
                            latestLuckyPlayer.text = "Latest Lucky Player: Player " + (lucky.playerIndex + 1) + "was given a pair of Goggles.";
                        }
                    }
                }

                currentTurn = 0;
            }

            // If skipped, remove skip and keep looping
            if (skippedPlayers.Contains(currentTurn))
            {
                Debug.Log("Skipping Player " + currentTurn);

                skippedPlayers.Remove(currentTurn);
            }
            else
            {
                break;
            }

        } while (true);
    }
}
