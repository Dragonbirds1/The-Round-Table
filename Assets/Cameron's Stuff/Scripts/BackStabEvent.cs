using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class BackStabEvent : MonoBehaviour
{
    /// <summary>
    /// This script will be for the backstab event.
    /// What this script will do is start the backstab event.
    /// It will include cutting the power and the ability to kill.
    /// </summary>

    [Header("Settings")]
    public GameObject currentLight, lampLight1, lampLight2, lampLight3;
    public GameObject targetLight, blackoutLight;
    public GameObject wol;
    public GameObject StabberText;
    public TextMeshProUGUI textMeshProUGUI;
    public KeyCode backStabKey;
    public float timerForStab;
    public float timeTillStart;
    public bool random;
    bool StartTimer1 = false;
    bool StartTimer2 = false;
    public string player1Stab;
    public string player2Stab;
    public string player3Stab;
    public string player4Stab;
    public List<PlayerInput> players = new List<PlayerInput>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetLight.SetActive(false);
        blackoutLight.SetActive(false);
        random = true;
        StabberText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StartTimer1 == true)
        {
            timerForStab -= Time.deltaTime;
            if (timerForStab <= 0)
            {
                timerForStab = 30f;
                currentLight.SetActive(true);
                lampLight1.SetActive(true);
                lampLight2.SetActive(true);
                lampLight3.SetActive(true);
                targetLight.SetActive(false);
                wol.SetActive(true);
                StartTimer1 = false;
                random = true;
                StabberText.SetActive(false);
            }
        }
        if (StartTimer2 == true)
        {
            timeTillStart -= Time.deltaTime;
            if (timeTillStart <= 0)
            {
                timeTillStart = 2f;
                StabberText.SetActive(true);
                if (random == true)
                {
                    int winner = Random.Range(0, players.Count);
                    if (winner == 0)
                    {
                        textMeshProUGUI.text = player1Stab;
                        random = false;
                    }
                    if (winner == 1)
                    {
                        textMeshProUGUI.text = player2Stab;
                        random = false;
                    }
                    if (winner == 2)
                    {
                        textMeshProUGUI.text = player3Stab;
                        random = false;
                    }
                    if (winner == 3)
                    {
                        textMeshProUGUI.text = player4Stab;
                        random = false;
                    }
                }
                currentLight.SetActive(false);
                lampLight1.SetActive(false);
                lampLight2.SetActive(false);
                lampLight3.SetActive(false);
                blackoutLight.SetActive(false);
                targetLight.SetActive(true);
                Random.Range(1, 4);
                wol.SetActive(false);
                StartTimer1 = true;
                StartTimer2 = false;
            }
        }
        if (Input.GetKeyDown(backStabKey))
        {
            blackoutLight.SetActive(true);
            StartTimer2 = true;
        }
    }
}
