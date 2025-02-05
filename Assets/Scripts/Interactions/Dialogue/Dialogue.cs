using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public static Dialogue Instance;
    public TextMeshProUGUI dialogueUI;
    private List<string> _lines;
    public float textSpeed;

    public bool importantTextOnDisplay = false;

    private int _index;
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (PlayerInventory.Instance.isInventoryActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueUI.text.Length > 0)
            {
                if (dialogueUI.text == _lines[_index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueUI.text = _lines[_index];
                }
            }
        }
    }

    void SetLines(List<string> lines)
    {
        _lines = lines;
    }

    public void StartDialogue(List<string> lines, bool important)
    {
        importantTextOnDisplay = important;
        SetLines(lines);
        _index = 0;
        StartCoroutine(TypeLine());
    }

    public void ClearDialogue()
    {
        StartCoroutine(ClearLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in _lines[_index].ToCharArray())
        {
            dialogueUI.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator ClearLine()
    {
        if (dialogueUI.text.Length < 1) yield return null;

        for (int i = dialogueUI.text.Length - 1; i >= 0; i--)
        {
            dialogueUI.text = dialogueUI.text.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }

    IEnumerator TypeAndClear()
    {
        yield return StartCoroutine(ClearLine());

        yield return StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (_index < _lines.Count - 1)
        {
            _index++;
            StartCoroutine(TypeAndClear());
        }
        else
        {
            ClearDialogue();
            importantTextOnDisplay = false;
        }
    }
}
