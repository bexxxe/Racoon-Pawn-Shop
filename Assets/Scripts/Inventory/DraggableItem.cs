using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
{
    public Image itemImage;
    public ItemData itemData;
    [HideInInspector] public Transform originalParent;

    void Start() 
    {
        if (itemData != null) 
        {
            itemImage.sprite = itemData.itemSprite;
        }
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        originalParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        itemImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("OnDrag");
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("OnEndDrag");
        transform.SetParent(originalParent);
        itemImage.raycastTarget = true;
    }
}
