using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTagName : MonoBehaviour
{
    public static InventoryTagName Instance;
    public GameObject interactionUI;
    public GameObject optionsUI;
    public TextMeshProUGUI interactionText;
    public Item selectedItem;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!PlayerInventory.Instance.isInventoryActive) return;
        ScreenMouseRay();
    }

    public void ScreenMouseRay()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        bool hitSomething = false;
        bool isBlocked = false;
        
        foreach (var result in results)
        {
            //Debug.Log(result.gameObject.name);
            IInteractable interactable = result.gameObject.GetComponent<IInteractable>();
            if (result.gameObject.name == "ItemOptionsUI") isBlocked = true;

            if (interactable != null && !isBlocked)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    optionsUI.transform.position = Input.mousePosition;
                    optionsUI.SetActive(true);
                    interactable.Interact();
                }
            }
        }

        interactionUI.SetActive(hitSomething);
        interactionUI.transform.position = Input.mousePosition;
    }

    public void EquipItem()
    {
        if(selectedItem != null)
        {
            PlayerInventory.Instance.GetItemToHand(selectedItem);
        }
    }
    
    public void InspectItem()
    {
        if (selectedItem != null)
        {
            PlayerInventory.Instance.ToggleInspect();
            Item3DViewer.Instance.SpawnPrefabInViewer(selectedItem);
        }
    }
}
