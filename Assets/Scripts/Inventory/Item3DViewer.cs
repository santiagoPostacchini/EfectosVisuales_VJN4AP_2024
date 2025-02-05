using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item3DViewer : MonoBehaviour, IDragHandler
{
    public static Item3DViewer Instance;
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
        itemPrefab.transform.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
    }
}
