using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Pawn Shop/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public string correctDialogue;
    public string wrongDialogue;
    public int value;
}
