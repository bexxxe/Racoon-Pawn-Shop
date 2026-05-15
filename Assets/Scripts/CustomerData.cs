using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[CreateAssetMenu(fileName = "NewCustomer", menuName = "Pawn Shop/Customer Data")]
public class CustomerData : ScriptableObject
{
    public string customerName;
    public Sprite customerSprite;
    public string requestDialogue;
    public ItemData requestedItem;
    public RuntimeAnimatorController animatorController;
    public TimelineAsset timeline;
    public GameObject customerPrefab;
}
