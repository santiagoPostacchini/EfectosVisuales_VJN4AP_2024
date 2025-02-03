using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTagName : MonoBehaviour
{
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

    void Update()
    {
        if (!PlayerInventory.Instance.isActive) return;
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

        foreach (var result in results)
        {
            //Debug.Log(result.gameObject.name);
            IInteractable interactable = result.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    interactable.Interact();
                }
            }
        }

        interactionUI.SetActive(hitSomething);
        interactionUI.transform.position = Input.mousePosition;
    }
}
