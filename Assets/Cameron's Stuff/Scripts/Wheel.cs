using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Wheel : MonoBehaviour
{
    public Transform spinPart;

    private bool spinning;

    //------------------------------------------------
    // CALL THIS FROM THE MINIGAME
    //------------------------------------------------

    public void SpinForPlayer(PlayerInput winner)
    {
        if (spinning) return;

        StartCoroutine(SpinRoutine(winner));
    }

    //------------------------------------------------

    IEnumerator SpinRoutine(PlayerInput winner)
    {
        spinning = true;

        //------------------------------------------------
        // Pick a RESULT instead of a player
        //------------------------------------------------

        int resultIndex = Random.Range(0, 4);

        float sliceSize = 360f / 4f;

        float targetAngle = (resultIndex * sliceSize) + (sliceSize / 2f);

        float totalSpin = 360f * Random.Range(5, 8) + targetAngle;

        float duration = 5f;
        float timer = 0f;

        float startRot = spinPart.eulerAngles.z;
        float endRot = startRot - totalSpin;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            // Casino easing
            t = 1 - Mathf.Pow(1 - t, 4);

            float rot = Mathf.Lerp(startRot, endRot, t);
            spinPart.rotation = Quaternion.Euler(0, 0, rot);

            yield return null;
        }

        spinPart.rotation = Quaternion.Euler(0, 0, endRot);

        Debug.Log("RESULT INDEX: " + resultIndex);

        //------------------------------------------------
        // Apply the result
        //------------------------------------------------

        ApplyWheelResult(resultIndex, winner);

        spinning = false;
    }

    //------------------------------------------------
    // RESULTS
    //------------------------------------------------

    void ApplyWheelResult(int result, PlayerInput winner)
    {
        switch (result)
        {
            case 0:
                Debug.Log("WINNER GETS +1 ITEM!");
                break;

            case 1:
                Debug.Log("WINNER BECOMES THE STABBER!");
                FindFirstObjectByType<BackStabEvent>().ForceStabber(winner);
                break;

            case 2:
                Debug.Log("WINNER GETS +2 ITEMS!");
                break;

            case 3:
                Debug.Log("NOTHING HAPPENS!");
                break;
        }
    }
}
