using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public Item[] testItem;

    public void PickupItem()
    {
        bool result = InventoryManager.Instance.AddItem(testItem[Random.Range(0, testItem.Length - 1)]);
        if(result) 
        {
            Debug.Log("Item Added");
        }
        else
        {
            Debug.Log("Item not Added");
        }
    }
}
