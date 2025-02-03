using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireIgnition : MonoBehaviour, IInteractable
{
    public GameObject fires;
    public bool isOn = true;
    public string[] lines;

    public string GetDescription()
    {
        string text = isOn ? "Turn Off" : "Turn On";
        return text;
    }

    public void Interact()
    {
        isOn = !isOn;
        fires.SetActive(isOn);
        string l = isOn ? lines[0] : lines[1];
        var a = new List<string> { l };
        Dialogue.Instance.StartDialogue(a, true);
    }
}
