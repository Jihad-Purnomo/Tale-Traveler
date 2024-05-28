using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Player Player;
    private Movement Movement;
    public CameraLogic Camera { get; private set; }

    [SerializeField] private float speed;
    [SerializeField] private float duration;

    private Vector2 direction;
    private float timer;

    private void Awake()
    {
        Player = GetComponentInParent<Player>();
        Movement = GetComponentInParent<Movement>();
        Camera = FindObjectOfType<CameraLogic>();
    }

    private void OnEnable()
    {
        direction = new Vector2(1, Input.Move.y);
        timer = duration;
        Camera.SetFollow(transform);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            Player.DeactivateSpell();
        }
    }

    private void OnDisable()
    {
        transform.position = transform.parent.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Player.DeactivateSpell();
        }

        if (collision.gameObject.CompareTag("Sticker"))
        {
            gameObject.SetActive(false);

            switch (Player.selectedSpell)
            {
                case Player.SpellType.Telekinesis:
                    Movement.ActivateObject(collision.GetComponentInParent<ObjectData>());
                    Camera.SetFollow(collision.transform);
                    break;
                case Player.SpellType.Duplicate:
                    break;
            }
        }
    }
}
