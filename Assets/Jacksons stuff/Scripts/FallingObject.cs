using UnityEngine;
using UnityEngine.InputSystem;

public class FallingObject : MonoBehaviour
{
    private RockMinigameManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<RockMinigameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerInput player =
            collision.gameObject.GetComponent<PlayerInput>();

        if (player != null)
        {
            manager.PlayerHit(player);

            Destroy(gameObject);
        }
    }
}
