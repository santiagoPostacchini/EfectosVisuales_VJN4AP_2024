using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public GameObject inventoryUI;
    public GameObject optionsUI;
    public GameObject inspectUI;
    public KeyCode toggleKey;
    public KeyCode useKey;
    public KeyCode backKey;
    public Transform objectHolder;
    [HideInInspector] public bool isInventoryActive = false;
    [HideInInspector] public bool isInspectActive = false;
    [HideInInspector] public Item itemInHand;
    [HideInInspector] public GameObject itemInHandPrefab;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey) && !isInspectActive)
        {
            ToggleInventory();
            SetCursorLock(isInventoryActive);
        }
        if (Input.GetKeyDown(backKey) && !isInventoryActive && isInspectActive)
        {
            ToggleInspect();
        }
        if(Input.GetKeyDown(useKey))
        {
            if (itemInHand != null)
            {
                if (isInventoryActive) return;
                if (PlayerInteraction.Instance.interactionUI.activeSelf) return;
                itemInHand.prefab.GetComponent<ObjectBehaviour>().Use();
            }
        }
    }

    void ToggleInventory()
    {
        isInventoryActive = !isInventoryActive;
        inventoryUI.SetActive(isInventoryActive);
        optionsUI.SetActive(false);
    }

    public void ToggleInspect()
    {
        isInspectActive = !isInspectActive;
        ToggleInventory();
        inspectUI.SetActive(isInspectActive);
        Item3DViewer.Instance.ToggleCamera();
    }

    void SetCursorLock(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = b;
    }

    public void GetItemToHand(Item item)
    {
        if (item.prefab)
        {
            StoreCurrentItem();
            itemInHand = item;
            itemInHandPrefab = Instantiate(itemInHand.prefab);
            itemInHandPrefab.SetActive(true);
            itemInHandPrefab.transform.SetParent(objectHolder.transform);
            itemInHandPrefab.GetComponent<ObjectBehaviour>().GetToPosition(objectHolder);
            itemInHandPrefab.GetComponent<ObjectBehaviour>().ChangeLayer();
        }
        else
        {
            Debug.Log("No asignaste un prefab al scriptable");
        }
    }

    public void StoreCurrentItem()
    {
        if (itemInHand != null)
        {
            Destroy(itemInHandPrefab);
        }
    }
}
