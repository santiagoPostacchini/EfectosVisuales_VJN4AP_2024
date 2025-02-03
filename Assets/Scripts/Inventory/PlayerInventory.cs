using UnityEngine;


public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;
    public GameObject inventory;
    public GameObject inventoryUITag;
    public KeyCode toggleKey;
    public KeyCode useKey;
    public Transform objectHolder;
    [HideInInspector] public bool isActive = false;
    [HideInInspector] public Item itemInHand;
    [HideInInspector] public GameObject itemInHandPrefab;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isActive = !isActive;
            inventory.SetActive(isActive);
            inventoryUITag.SetActive(isActive);
            SetCursorLock(isActive);
        }
        if(Input.GetKeyDown(useKey))
        {
            if (itemInHand != null)
            {
                if (isActive) return;
                if (PlayerInteraction.Instance.interactionUI.activeSelf) return;
                itemInHand.prefab.GetComponent<ObjectBehaviour>().Use();
            }
        }
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
