using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public bool isMat = false;
    public ItemData requiredItem;
    public DialogueManager dialogueManager;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject droppedItem = eventData.pointerDrag;
            DraggableItem draggableItem = droppedItem.GetComponent<DraggableItem>();
            draggableItem.originalParent = transform;

            if (isMat)
            {
                bool isCorrect = draggableItem.itemData == requiredItem;
                string dialogue = isCorrect ?
                requiredItem.correctDialogue :
                draggableItem.itemData.wrongDialogue;
                dialogueManager.ShowDialogue(isCorrect, dialogue);
            }
        }
    }
}
