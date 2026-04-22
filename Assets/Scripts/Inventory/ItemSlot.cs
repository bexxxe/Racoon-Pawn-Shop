using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
        public void OnDrop(PointerEventData eventData) 
        {
        if (transform.childCount == 0) {
        GameObject droppedItem = eventData.pointerDrag;
        DraggableItem draggableItem = droppedItem.GetComponent<DraggableItem>();
        draggableItem.originalParent = transform;
        }
       
    }
}
