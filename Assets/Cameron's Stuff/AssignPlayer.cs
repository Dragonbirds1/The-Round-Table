using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class AssignPlayer : MonoBehaviour
{
    public RuntimeAnimatorController[] baseAnim;
    public Vector3[] position;
    public Teleport teleport;

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

        FindFirstObjectByType<RockMinigameManager>().players.Add(input);

        

        //------------------------------------------------
        // OPTIONAL — Start turns once players join
        //------------------------------------------------

        MainGameManager.Instance.ChangeState(MainGameManager.GameState.Turns);

        //------------------------------------------------

        input.DeactivateInput();
    }

}
