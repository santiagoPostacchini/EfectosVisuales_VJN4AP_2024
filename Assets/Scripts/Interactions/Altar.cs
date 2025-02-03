using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Altar : MonoBehaviour, IInteractable
{
    public string[] lines;

    public int gemAmmount = 0;
    public string GetDescription()
    {
        return "Interact";
    }

    public void Interact()
    {
        //IF tengo piedra no dialogo, ELSE, dialogo x * cantpiedras
        List<string> list = new List<string> { lines[gemAmmount] };
        Dialogue.Instance.StartDialogue(list, true);
    }
}
