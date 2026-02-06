using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class AssignPlayer : MonoBehaviour
{
    public AnimatorController[] baseAnim;
    public Vector3[] position;

    public void Assign(PlayerInput input)
    {
        input.GetComponent<Animator>().runtimeAnimatorController = baseAnim[input.playerIndex];
        input.transform.position = position[input.playerIndex];
        //input.DeactivateInput();
    }
}
