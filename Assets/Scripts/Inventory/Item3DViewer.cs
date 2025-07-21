using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    public static Item3DViewer Instance;
    public Material blurMat;
    private string _blurAmmName = "_BlurStrength";
    public Camera inspectCamera;
    public Camera handCamera;
    private GameObject itemPrefab;
    private bool active = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleCamera()
    {
        active = !active;
        inspectCamera.gameObject.SetActive(active);
        handCamera.gameObject.SetActive(!active);
        ToggleBlur();
    }

    public void SpawnPrefabInViewer(Item item)
    {
        Debug.Log(item.name);
        if (itemPrefab != null)
        {
            Destroy(itemPrefab.gameObject);
        }
        itemPrefab = Instantiate(item.prefab, new Vector3(1000, 1000, 1000), Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float rotationSpeed = 1f; // Adjust this value to make rotation smoother
        Vector3 rotation = new Vector3(-eventData.delta.y, -eventData.delta.x, 0) * rotationSpeed;

        itemPrefab.transform.rotation = Quaternion.Euler(rotation) * itemPrefab.transform.rotation;
    }


    public void ToggleBlur()
    {
        float val = active ? 0.005f : 0.0f;
        blurMat.SetFloat(_blurAmmName, val);
    }
}
