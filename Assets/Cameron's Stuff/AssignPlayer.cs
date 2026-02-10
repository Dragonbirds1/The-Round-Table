using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class AssignPlayer : MonoBehaviour
{
    public AnimatorController[] baseAnim;
    public Vector3[] position;

    public void Assign(PlayerInput input)
    {
        Animator anim = input.GetComponent<Animator>();

        if (anim != null)
        {
            anim.runtimeAnimatorController = baseAnim[input.playerIndex];
        }


        input.transform.position = position[input.playerIndex];

        FindFirstObjectByType<Wheel>().players.Add(input);
        
        FindFirstObjectByType<BackStabEvent>().players.Add(input);

        FindFirstObjectByType<Turns>().players.Add(input);


        input.DeactivateInput();
    }
}
