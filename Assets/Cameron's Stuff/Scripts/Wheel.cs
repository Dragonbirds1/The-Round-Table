using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Wheel : MonoBehaviour
{
    public Transform spinPart;

    public Turns turns;

    private bool spinning;

    [Header("Wheel Gizmos")]
    public int sliceCount = 4;
    public float gizmoRadius = 3f;
    public float gizmoOffset = 0f; // adjust if arrow isn't at top
    public float sliceOffset = 45f;

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

        float totalSpin = Random.Range(2000f, 3500f);

        float duration = 5f;
        float timer = 0f;

        float startRot = spinPart.eulerAngles.z;
        float endRot = startRot - totalSpin;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float t = timer / duration;

            // smooth casino slowdown
            t = 1 - Mathf.Pow(1 - t, 4);

            float rot = Mathf.Lerp(startRot, endRot, t);

            spinPart.rotation = Quaternion.Euler(0, 0, rot);

            yield return null;
        }

        //------------------------------------------------
        // Wheel finished spinning
        //------------------------------------------------

        spinPart.rotation = Quaternion.Euler(0, 0, endRot);

        // ⭐ GET RESULT FROM ARROW
        int resultIndex = GetWheelResult();

        Debug.Log("LANDED ON SLICE: " + resultIndex);

        // ⭐ APPLY RESULT
        yield return new WaitForSeconds(1f);
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

        // ✅ EXIT MINIGAME ONCE
        turns.miniGame = false;
    }

    int GetWheelResult()
    {
        float sliceSize = 360f / sliceCount;

        // Convert clockwise rotation into arrow direction
        float corrected = (360 - spinPart.eulerAngles.z) % 360f;

        int slice = Mathf.FloorToInt(corrected / sliceSize);

        // Safety clamp
        slice = Mathf.Clamp(slice, 0, sliceCount - 1);

        return slice;
    }

    private void OnDrawGizmos()
    {
        if (spinPart == null) return;

        Vector3 center = spinPart.position;

        float sliceAngle = 360f / sliceCount;

        //------------------------------------------------
        // Draw outer circle
        //------------------------------------------------
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center, gizmoRadius);

        //------------------------------------------------
        // Draw slice borders
        //------------------------------------------------
        for (int i = 0; i < sliceCount; i++)
        {
            float angle = (sliceAngle * i) + sliceOffset;

            float rad = angle * Mathf.Deg2Rad;

            Vector3 dir =
                new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0);

            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(center, center + dir * gizmoRadius);
        }

        //------------------------------------------------
        // Draw slice centers (VERY useful)
        //------------------------------------------------
        for (int i = 0; i < sliceCount; i++)
        {
            float angle = (sliceAngle * i) + (sliceAngle / 2f) + sliceOffset;

            float rad = angle * Mathf.Deg2Rad;

            Vector3 dir =
                new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0);

            Gizmos.color = Color.green;

            Gizmos.DrawLine(center, center + dir * (gizmoRadius));
        }

        //------------------------------------------------
        // Draw arrow direction
        //------------------------------------------------
        Gizmos.color = Color.red;

        Vector3 arrowDir = Vector3.up; // assumes arrow is TOP

        Gizmos.DrawLine(center, center + arrowDir * gizmoRadius);
    }
}
