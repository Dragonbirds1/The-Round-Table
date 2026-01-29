using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Scriptable Objects/Item Data")]
public class itemData : ScriptableObject
{
    public Sprite sprite;
   [TextArea] public string description;
}
