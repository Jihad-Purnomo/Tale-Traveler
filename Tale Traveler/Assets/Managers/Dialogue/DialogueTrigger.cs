using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coll.enabled = false;
            DialogueManager.Inst.StartDialogue(dialogue);
        }
    }
}
