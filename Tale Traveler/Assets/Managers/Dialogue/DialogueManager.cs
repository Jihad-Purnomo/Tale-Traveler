using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Inst;

    public Image CharProfile;
    public TextMeshProUGUI NameArea;
    public TextMeshProUGUI TextArea;

    private Vector3 CharProfilePos;
    private Vector3 NameAreaPos;
    private Vector3 TextAreaPos;

    [Range(0.01f, 0.1f)] public float TextSpeed;

    private Queue<DialogueLine> lines;

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }

        CharProfilePos = CharProfile.rectTransform.position;
        NameAreaPos = NameArea.rectTransform.position;
        TextAreaPos = TextArea.rectTransform.position;
    }

    private void Update()
    {
        if (Input.MenuSubmit)
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
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

        if (currentLine.LeftSide)
        {
            SetLeftDialogue();
        }
        else
        {
            SetRightDialogue();
        }

        CharProfile.sprite = currentLine.Profile;
        NameArea.text = currentLine.Name;

        StopAllCoroutines();
        StartCoroutine(TypeLine(currentLine));
    }

    public void SetLeftDialogue()
    {
        CharProfile.rectTransform.position = CharProfilePos;
        TextArea.rectTransform.position = TextAreaPos;
        NameArea.rectTransform.position = NameAreaPos;
    }
    
    public void SetRightDialogue()
    {
        CharProfile.rectTransform.position = new Vector3(-CharProfilePos.x, CharProfilePos.y);
        TextArea.rectTransform.position = new Vector3(-TextAreaPos.x, TextAreaPos.y);
        NameArea.rectTransform.position = new Vector3(-NameAreaPos.x, NameAreaPos.y);
    }

    IEnumerator TypeLine(DialogueLine currentLine)
    {
        TextArea.text = "";
        foreach(char letter in currentLine.Line.ToCharArray())
        {
            TextArea.text += letter;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    public void EndDialogue()
    {
        Input.ChangeActionMap("Gameplay");
    }
}

public class DialogueLine
{
    public bool LeftSide;
    public Sprite Profile;
    public string Name;
    [TextArea(3, 10)] public string Line;
}

public class Dialogue
{
    public List<DialogueLine> DialogueLines = new List<DialogueLine>();
}
