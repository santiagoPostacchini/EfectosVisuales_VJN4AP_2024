using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction Instance;
    public Camera mainCam;
    public float interactionDistance = 2f;

    public GameObject interactionUI;
    public GameObject crosshair;
    public TextMeshProUGUI interactionText;
    public LayerMask interactionLayerMask;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        InteractionRay();
    }

    void InteractionRay()
    {
        if (Dialogue.Instance.importantTextOnDisplay) return;
        if (PlayerInventory.Instance.isActive) return;

        Ray ray = mainCam.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitSomething = false;

        if(Physics.Raycast(ray, out hit, interactionDistance, interactionLayerMask))
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;
                interactionText.text = interactable.GetDescription();
                if(Input.GetKeyDown(KeyCode.E)) 
                {
                    interactable.Interact();
                }
            }
        }

        interactionUI.SetActive(hitSomething);
        crosshair.SetActive(!hitSomething);
    }
}
