using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Angel : MonoBehaviour, IInteractable
{
    public string[] lines;
    public Altar altar;
        
    public string GetDescription()
    {
        return "Interact";
    }

    public void Interact()
    {
        List<string> list = new List<string> { lines[altar.gemAmmount] }; //sigue en dialogos al altar
        Dialogue.Instance.StartDialogue(list, true);
    }
}
