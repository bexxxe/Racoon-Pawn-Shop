using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public bool isMat = false;
    public ItemData requiredItem;
    public DialogueManager dialogueManager;
    public bool isInteractable = false;

    public void OnDrop(PointerEventData eventData)
    {
        if (isMat && !isInteractable) return;
        if (eventData.pointerDrag == null) return;

        GameObject draggedObject = eventData.pointerDrag;
        DraggableItem draggableItem = draggedObject.GetComponent<DraggableItem>();

        if (transform.childCount == 0)
        {
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
        else if (isMat)
        {
            Debug.Log("Slot already has an item. Cannot drop another.");
            Transform existingItem = transform.GetChild(0);
            DraggableItem existingDraggable = existingItem.GetComponent<DraggableItem>();
            existingItem.SetParent(existingDraggable.originalParent);

            draggableItem.originalParent = transform;

            bool isCorrect = draggableItem.itemData == requiredItem;
            string dialogue = isCorrect ?
                requiredItem.correctDialogue :
                draggableItem.itemData.wrongDialogue;
            dialogueManager.ShowDialogue(isCorrect, dialogue);
        }
    }
}
