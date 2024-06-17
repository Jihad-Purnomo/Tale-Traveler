using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDataPersistence
{
    private ObjectData Object;
    private Movement Movement;
    [SerializeField] private Spell Spell;

    private Animator anim;

    private int facing;

    private bool hasTelekinesis = false;
    private bool hasDuplicate = false;

    private bool isTouchingSticker = false;
    private Transform touchedSticker;
    private Vector3 stickerDistance;

    [SerializeField] private float dragMult;

    private enum PlayerState { Busy = 0, Standby = 1, Dragging = 2, Spellcasting = 3 }
    public enum SpellType { None = 0, Telekinesis = 1, Duplicate = 2 }

    private PlayerState currentState;
    public SpellType selectedSpell { get; private set; }

    private void Awake()
    {
        Object = GetComponent<ObjectData>();
        Movement = GetComponent<Movement>();

        anim = GetComponent<Animator>();
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
                if (Object.Rb.velocity.y > 0f)
                {
                    anim.Play("PlayerJump");
                }

                if (Object.Rb.velocity.y < 0f)
                {
                    anim.Play("PlayerFall");
                }

                if (Movement.lastOnGroundTime > 0f)
                {
                    currentState = PlayerState.Standby;
                }
                break;

            case PlayerState.Standby:
                if (Input.Move.x == 0)
                {
                    anim.Play("PlayerIdle");
                }
                else
                {
                    anim.Play("PlayerRun");
                }

                if (Input.Move.x == -facing)
                {
                    Turn();
                }

                if (Movement.lastOnGroundTime <= 0f)
                {
                    currentState = PlayerState.Busy;
                }

                if (Input.GrabHeld && isTouchingSticker)
                {
                    Input.DisableAction(Input.jumpAction);
                    stickerDistance = touchedSticker.parent.position - transform.position;
                    currentState = PlayerState.Dragging;
                }

                if (Input.SpellPressed && hasTelekinesis)
                {
                    currentState = PlayerState.Spellcasting;
                    Spell.gameObject.SetActive(true);
                }
                break;

            case PlayerState.Dragging:
                Object.Rb.velocity = new Vector2(Object.Rb.velocity.x * dragMult, Object.Rb.velocity.y);
                touchedSticker.parent.position = transform.position + stickerDistance;
                
                if (Input.Move.x == 0f)
                {
                    anim.Play("PlayerGrab");
                }
                else
                {
                    if (Mathf.Sign(Input.Move.x) == Mathf.Sign(facing)) anim.Play("PlayerPush");
                    else anim.Play("PlayerPull");
                }

                if (Input.GrabReleased)
                {
                    Input.EnableAction(Input.jumpAction);
                    currentState = PlayerState.Standby;
                }
                break;

            case PlayerState.Spellcasting:
                Object.Rb.velocity = Vector2.zero;
                anim.Play("PlayerSpellcasting");

                if (Input.SpellReleased)
                {
                    DeactivateSpell();
                }                
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StickerBase") && !isTouchingSticker)
        {
            isTouchingSticker = true;
            touchedSticker = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StickerBase"))
        {
            isTouchingSticker = false;
        }
    }

    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData(ref GameData data)
    {
        data.playerPosition = this.transform.position;
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
