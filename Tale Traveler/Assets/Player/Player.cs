using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ObjectData Object;
    private Movement Movement;
    [SerializeField] private Spell Spell;

    private int facing;

    private bool hasTelekinesis = false;
    private bool hasDuplicate = false;

    private enum PlayerState { Busy = 0, Standby = 1, Dragging = 2, Spellcasting = 3 }
    public enum SpellType { None = 0, Telekinesis = 1, Duplicate = 2 }

    private PlayerState currentState;
    public SpellType selectedSpell { get; private set; }

    private void Awake()
    {
        Object = GetComponent<ObjectData>();
        Movement = GetComponent<Movement>();
    }

    private void Start()
    {
        Movement.ActivateObject(Object);
        GetSpell(SpellType.Telekinesis);
        facing = 1;
    }

    private void Update()
    {
        if (Input.ChangeSpell)
        {
            switch (selectedSpell)
            {
                case SpellType.None:
                    if (hasTelekinesis)
                    {
                        selectedSpell = SpellType.Telekinesis;
                    }
                    break;

                case SpellType.Telekinesis:
                    if (hasDuplicate)
                    {
                        selectedSpell = SpellType.Duplicate;
                    }
                    break;

                case SpellType.Duplicate:
                    selectedSpell = SpellType.Telekinesis;
                    break;
            }
        }

        switch (currentState)
        {
            case PlayerState.Busy:
                if (Movement.lastOnGroundTime > 0f)
                {
                    currentState = PlayerState.Standby;
                }
                break;

            case PlayerState.Standby:             
                if (Input.Move.x == -facing)
                {
                    Turn();
                }
                if (Movement.lastOnGroundTime <= 0f)
                {
                    currentState = PlayerState.Busy;
                }
                if (Input.SpellPressed && hasTelekinesis)
                {
                    currentState = PlayerState.Spellcasting;
                    Spell.gameObject.SetActive(true);
                }
                break;

            case PlayerState.Dragging:
                break;

            case PlayerState.Spellcasting:
                Object.Rb.velocity = Vector2.zero;

                if (Input.SpellReleased)
                {
                    DeactivateSpell();
                }                
                break;
        }
    }

    public void GetSpell(SpellType spell)
    {
        switch (spell)
        {
            case SpellType.Telekinesis:
                hasTelekinesis = true;
                selectedSpell = SpellType.Telekinesis;
                break;

            case SpellType.Duplicate:
                hasDuplicate = true;
                selectedSpell = SpellType.Duplicate;
                break;
        }
    }

    public void DeactivateSpell()
    {
        if (Spell.gameObject.activeSelf)
        {
            Spell.gameObject.SetActive(false);
        }

        if (Movement.Object != Object)
        {
            Movement.ActivateObject(Object);
        }

        Spell.Camera.SetFollow(transform);
        currentState = PlayerState.Standby;
    }

    public void Turn()
    {
        transform.Rotate(0, 180, 0);
        facing *= -1;
    }
}
