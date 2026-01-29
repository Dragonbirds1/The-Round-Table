using UnityEngine;

[RequireComponent (typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class ItemInstance : MonoBehaviour
{
    public itemData data;

    private SpriteRenderer _sprt;

    private void Awake()
    {
        _sprt = GetComponent<SpriteRenderer>();
        _sprt.sprite = data.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inventory.items.Add(data);
        Destroy(gameObject);
    }
}
