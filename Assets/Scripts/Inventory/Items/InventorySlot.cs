using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IInteractable
{
    public string GetDescription()
    {
        if(transform.childCount != 0)
        {
            return transform.GetChild(0).GetComponent<InventoryItem>().item.name;
        }
        return "";
    }

    public void Interact()
    {
        Item recivedItem = InventoryManager.Instance.GetSelectedItem(this, false);
        InventoryTagName.Instance.selectedItem = recivedItem;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
