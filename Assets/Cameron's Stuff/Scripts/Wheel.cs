using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Wheel : MonoBehaviour
{
    public Transform spinPart;
    public List<PlayerInput> players = new List<PlayerInput>();

    private bool spinning;

    void Update()
    {
        if (spinning) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame || AnyGamepadPressed())
        {
            if (players.Count == 0)
            {
                Debug.Log("No players!");
                return;
            }

            int winner = Random.Range(0, players.Count);
            StartCoroutine(SpinToWinner(winner));
        }
    }

    bool AnyGamepadPressed()
    {
        foreach (var pad in Gamepad.all)
        {
            if (pad.buttonSouth.wasPressedThisFrame)
                return true;
        }
        return false;
    }

    IEnumerator SpinToWinner(int winnerIndex)
    {
        spinning = true;

        int playerCount = players.Count;

        float sliceSize = 360f / playerCount;

        // Aim for the CENTER of the slice
        float targetAngle = (winnerIndex * sliceSize) + (sliceSize / 2f);

        // Add dramatic spins
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

        Debug.Log("WINNER: Player " + winnerIndex);

        StartCoroutine(SpinPlayer(players[winnerIndex].transform));

        spinning = false;
    }

    IEnumerator SpinPlayer(Transform player)
    {
        float duration = 0.6f;
        float timer = 0f;

        float startRot = player.eulerAngles.z;
        float endRot = startRot + 720f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            float rot = Mathf.Lerp(startRot, endRot, timer / duration);
            player.rotation = Quaternion.Euler(0, 0, rot);

            yield return null;
        }
    }
}
