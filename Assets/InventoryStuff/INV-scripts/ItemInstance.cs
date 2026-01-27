using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(BoxCollider2D))]

public class ItemInstance : MonoBehaviour
{
    public ItemData data;

    private SpriteRenderer _sprt;

    public void Awake()
    {
        _sprt = GetComponent<SpriteRenderer>();
        _sprt.sprite = data.sprite;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
