using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleLiquid : ObjectBehaviour
{
    public List<string> lines = new List<string>();
    public override void Use()
    {
        Debug.Log("Im a bottle");
    }

    public override void Interact()
    {
        InventoryManager.Instance.AddItem(Item);
        Destroy(this.gameObject);
        Dialogue.Instance.StartDialogue(lines, true);
    }

    public override string GetDescription()
    {
        return "Take";
    }
}
