using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackStabEvent : MonoBehaviour
{
    [Header("Lights")]
    public GameObject currentLight, lampLight1, lampLight2, lampLight3;
    public GameObject targetLight, blackoutLight;
    public GameObject wol;

    [Header("UI")]
    public GameObject StabberText;
    public TextMeshProUGUI textMeshProUGUI;

    [Header("Audio")]
    public AudioSource music;
    public AudioSource mainMusic1, mainMusic2;
    public AudioClip musicClip, lightsOut;

    [Header("Timers")]
    public float timerForStab = 32f;
    public float timeTillStart = 2f;

    [Header("Players")]
    public List<PlayerInput> players = new List<PlayerInput>();

    [Header("Impact FX")]
    public float hitStopTime = 0.08f;
    public float shakeDuration = 0.25f;
    public float shakeStrength = 0.15f;

    private Camera mainCam;
    private Vector3 camStartPos;

    private bool StartTimer1 = false;
    private bool StartTimer2 = false;
    private bool random = true;

    public bool start;
    public bool stabbing;
    public string winner2;

    public int winner;

    // ⭐ The chosen attacker
    private PlayerInput stabber;

    //------------------------------------------------

    void Start()
    {
        targetLight.SetActive(false);
        blackoutLight.SetActive(false);
        StabberText.SetActive(false);

        mainCam = Camera.main;
        camStartPos = mainCam.transform.position;
        winner = Random.Range(0, players.Count);
    }

    //------------------------------------------------

    void Update()
    {
        //--------------------------------
        // EVENT START
        //--------------------------------
        
        if (start)
        {
            StartTimer2 = true;
            wol.SetActive(false);
            music.PlayOneShot(lightsOut, 2);
            mainMusic1.enabled = false;
            mainMusic2.enabled = false;

            start = false;
        }

        //--------------------------------
        // BLACKOUT PHASE
        //--------------------------------
        if (StartTimer2)
        {
            blackoutLight.SetActive(true);

            currentLight.SetActive(false);
            lampLight1.SetActive(false);
            lampLight2.SetActive(false);
            lampLight3.SetActive(false);

            timeTillStart -= Time.deltaTime;

            if (timeTillStart <= 0)
            {
                music.PlayOneShot(musicClip, 0.5f);

                timeTillStart = 2f;
                StabberText.SetActive(true);

                PickStabber();

                blackoutLight.SetActive(false);
                targetLight.SetActive(true);

                StartTimer1 = true;
                StartTimer2 = false;
                stabbing = true;
            }
        }

        //--------------------------------
        // STAB WINDOW TIMER
        //--------------------------------
        if (StartTimer1)
        {
            timerForStab -= Time.deltaTime;

            if (timerForStab <= 0)
            {
                ResetEvent();
            }
        }

        //--------------------------------
        // CHECK FOR ATTACK
        //--------------------------------
        if (stabbing && stabber != null)
        {
            TryStab();
        }
    }

    //------------------------------------------------
    // PICK RANDOM STABBER
    //------------------------------------------------

    void PickStabber()
    {
        if (!random || players.Count == 0) return;

        if (players.Count == 0) return;

        stabber = players[winner];

        textMeshProUGUI.text = "PLAYER " + (stabber.playerIndex + 1) + " IS THE STABBER";

        Debug.Log("PLAYER " + stabber.playerIndex + " IS THE STABBER!");

        random = false;
    }

    //------------------------------------------------
    // DETECT DIRECTIONAL ATTACK
    //------------------------------------------------

    void TryStab()
    {
        var device = stabber.devices[0];

        if (device is Gamepad pad)
        {
            PlayerInput victim = GetTargetFromButton(pad);

            if (victim != null)
            {
                Debug.Log("PLAYER " + stabber.playerIndex +
                          " STABBED PLAYER " + victim.playerIndex);

                StartCoroutine(StabImpact(victim));
            }
        }
    }

    //------------------------------------------------
    // MAP BUTTONS TO TABLE POSITIONS
    //------------------------------------------------

    PlayerInput GetTargetFromButton(Gamepad pad)
    {
        if (players.Count == 0) return null;

        int stabberIndex = players.IndexOf(stabber);
        int targetIndex = -1;

        if (pad.buttonNorth.wasPressedThisFrame) targetIndex = 0;
        if (pad.buttonEast.wasPressedThisFrame) targetIndex = 1;
        if (pad.buttonSouth.wasPressedThisFrame) targetIndex = 2;
        if (pad.buttonWest.wasPressedThisFrame) targetIndex = 3;

        // No button pressed
        if (targetIndex == -1)
            return null;

        // ⭐ CRITICAL SAFETY CHECK
        if (targetIndex >= players.Count)
            return null;

        // Prevent stabbing yourself
        if (targetIndex == stabberIndex)
            return null;

        return players[targetIndex];
    }

    //------------------------------------------------
    // IMPACT EFFECT
    //------------------------------------------------

    IEnumerator StabImpact(PlayerInput victim)
    {
        // Freeze frame
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(hitStopTime);
        Time.timeScale = 1f;

        // Camera shake
        float timer = 0;

        while (timer < shakeDuration)
        {
            timer += Time.deltaTime;

            Vector3 offset = Random.insideUnitSphere * shakeStrength;
            offset.z = camStartPos.z;

            mainCam.transform.position = camStartPos + offset;

            yield return null;
        }

        mainCam.transform.position = camStartPos;

        // Controller rumble (stabber only)
        if (stabber.devices[0] is Gamepad pad)
        {
            pad.SetMotorSpeeds(1f, 1f);
            yield return new WaitForSeconds(0.2f);
            pad.SetMotorSpeeds(0, 0);
            music.Stop();
            ResetEvent();
        }

        //--------------------------------
        // PUT YOUR KILL LOGIC HERE
        //--------------------------------

        stabbing = false;
        stabber = null;
    }

    public void ForceStabber(PlayerInput player)
    {
        stabber = player;
        Debug.Log("Forced stabber is Player " + player.playerIndex);
    }

    //------------------------------------------------
    // RESET EVENT
    //------------------------------------------------

    void ResetEvent()
    {
        winner = Random.Range(0, players.Count);

        music.PlayOneShot(lightsOut, 2);
        mainMusic1.enabled = true;
        mainMusic2.enabled = true;

        timerForStab = 32f;

        currentLight.SetActive(true);
        lampLight1.SetActive(true);
        lampLight2.SetActive(true);
        lampLight3.SetActive(true);

        targetLight.SetActive(false);
        wol.SetActive(true);

        StartTimer1 = false;
        random = true;
        stabbing = false;
        stabber = null;

        StabberText.SetActive(false);
    }
}
