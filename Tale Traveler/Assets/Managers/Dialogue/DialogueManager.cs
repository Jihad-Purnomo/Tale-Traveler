using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Inst;

    public GameObject DialogueBubble;
    public TextMeshProUGUI TextArea;
    public TextMeshProUGUI NameArea;

    [Range(0.01f, 0.1f)] public float TextIntervalBase;
    [Range(0.01f, 0.1f)] public float TextIntervalFaster;

    private float TextInterval;

    private Queue<DialogueLine> lines;

    private bool isTyping = false;
    public bool inDialogue { get; private set; }

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }

        lines = new Queue<DialogueLine>();

        TextInterval = TextIntervalBase;
    }

    private void Update()
    {
        if (isTyping)
        {
            if (Input.MenuSubmitHeld)
            {
                TextInterval = TextIntervalFaster;
            }
            else
            {
                TextInterval = TextIntervalBase;
            }
        }
        else
        {
            if (Input.MenuSubmit)
            {
                DisplayNextLine();
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        inDialogue = true;
        DialogueBubble.SetActive(true);
        Input.ChangeActionMap("UI");
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.DialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        NameArea.text = currentLine.Name;

        StopAllCoroutines();
        StartCoroutine(TypeLine(currentLine));
    }

    IEnumerator TypeLine(DialogueLine currentLine)
    {
        isTyping = true;

        TextArea.text = "";
        foreach(char letter in currentLine.Line.ToCharArray())
        {
            TextArea.text += letter;
            yield return new WaitForSeconds(TextInterval);
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        Input.ChangeActionMap("Gameplay");
        inDialogue = false;
        DialogueBubble.SetActive(false);
    }
}

[System.Serializable]
public class DialogueLine
{
    public string Name;
    [TextArea(3, 10)] public string Line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> DialogueLines = new List<DialogueLine>();
}
