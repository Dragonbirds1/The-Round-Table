using UnityEngine;
using UnityEngine.InputSystem;

public class FallingObject : MonoBehaviour
{
    private RockMinigameManager manager;

    void Start()
    {
        manager = FindFirstObjectByType<RockMinigameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerInput player =
            collision.gameObject.GetComponent<PlayerInput>();

            manager.PlayerHit(player);

            Destroy(gameObject);
    }
}
