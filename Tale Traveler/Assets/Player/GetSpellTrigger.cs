using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSpellTrigger : MonoBehaviour
{
    [SerializeField] private SpellType spellType;

    public Dialogue dialogue;

    private Collider2D coll;
    private SpriteRenderer sprite;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            coll.enabled = false;
            sprite.enabled = false;

            collision.GetComponent<Player>().GetSpell(spellType);

            DialogueManager.Inst.StartDialogue(dialogue);
        }
    }
}
