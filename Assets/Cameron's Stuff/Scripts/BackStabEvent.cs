using UnityEngine;
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
    public GameObject targetLight;
    public GameObject wol;
    public KeyCode backStabKey;
    public float timerForStab;
    bool StartTimer1 = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetLight.SetActive(false);
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
            }
        }
        if (Input.GetKeyDown(backStabKey))
        {
            currentLight.SetActive(false);
            lampLight1.SetActive(false);
            lampLight2.SetActive(false);
            lampLight3.SetActive(false);
            targetLight.SetActive(true);
            wol.SetActive(false);
            StartTimer1 = true;
        }
    }
}
