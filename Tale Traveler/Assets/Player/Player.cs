using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ObjectData Object;
    private Movement Movement;
    [SerializeField] private Collider2D Spell;

    private enum PlayerState { Busy = 0, Standby = 1, Spellcasting = 2 }
    private PlayerState currentState;

    public int FaceValue { get; private set; }

    private void Awake()
    {
        Object = GetComponent<ObjectData>();
        Movement = GetComponent<Movement>();
    }

    private void Start()
    {
        Movement.ActivateObject(Object);
        FaceValue = 1;
    }

    private void Update()
    {
        switch (currentState)
        {
            case PlayerState.Busy:
                if (Movement.lastOnGroundTime > 0f)
                {
                    currentState = PlayerState.Standby;
                }
                break;

            case PlayerState.Standby:
                if (Input.Move.x == -FaceValue)
                {
                    Turn();
                }

                if (Movement.lastOnGroundTime <= 0f)
                {
                    currentState = PlayerState.Busy;
                }

                if (Input.SpellPressed)
                {
                    currentState = PlayerState.Spellcasting;
                    Spell.gameObject.SetActive(true);
                }
                break;

            case PlayerState.Spellcasting:
                Object.Rb.velocity = Vector2.zero;

                if (Input.SpellReleased)
                {
                    Spell.gameObject.SetActive(false);
                    Movement.ActivateObject(Object);
                    currentState = PlayerState.Standby;
                }
                break;
        }
    }

    public void Turn()
    {
        transform.Rotate(0, 180, 0);
        FaceValue *= -1;
    }
}
