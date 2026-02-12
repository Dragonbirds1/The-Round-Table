using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using System.Collections.Generic;
using System.Collections;

public class AssignPlayer : MonoBehaviour
{
    public RuntimeAnimatorController[] baseAnim;
    public Vector3[] position;
    public Teleport teleport;
    public AudioSource audioSource;
    public AudioClip audioClip;

    public void Assign(PlayerInput input)
    {
        Animator anim = input.GetComponent<Animator>();

        if (anim != null)
        {
            anim.runtimeAnimatorController = baseAnim[input.playerIndex];
        }

        input.transform.position = position[input.playerIndex];

        //------------------------------------------------
        // REGISTER PLAYER (ONE MASTER LIST)
        //------------------------------------------------

        MainGameManager.Instance.RegisterPlayer(input);

        FindFirstObjectByType<BackStabEvent>().players.Add(input);

        FindFirstObjectByType<Teleport>().players.Add(input);

        FindFirstObjectByType<RockMinigameManager>().alivePlayers.Add(input);

        FindAnyObjectByType<Turns>().alivePlayers.Add(input);

        FindFirstObjectByType<RockMinigameManager>().players.Add(input);

        audioSource.PlayOneShot(audioClip, 1);

        

        //------------------------------------------------
        // OPTIONAL — Start turns once players join
        //------------------------------------------------

        MainGameManager.Instance.ChangeState(MainGameManager.GameState.Turns);

        //------------------------------------------------

        input.DeactivateInput();
    }

}
