using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.U2D.Aseprite;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssignPlayer : MonoBehaviour
{
    public AnimatorController[] baseAnim;
    public Vector3[] position;

    public void Assign(PlayerInput input)
    {
        input.GetComponent<Animator>().runtimeAnimatorController =
            baseAnim[input.playerIndex];

        input.transform.position = position[input.playerIndex];

        FindFirstObjectByType<Wheel>().players.Add(input);
        
        FindFirstObjectByType<BackStabEvent>().players.Add(input);
    }
}
