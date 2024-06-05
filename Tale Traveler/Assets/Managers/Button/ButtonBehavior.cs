using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private GameObject door;

    [SerializeField] private Sprite unpressedButton;
    [SerializeField] private Sprite pressedButton;

    [SerializeField] private Sprite openedDoor;
    [SerializeField] private Sprite closedDoor;

    private SpriteRenderer buttonRenderer;
    private SpriteRenderer doorRenderer;

    private Collider2D doorCollider;

    private void Awake()
    {
        buttonRenderer = GetComponent<SpriteRenderer>();
        doorRenderer = door.GetComponent<SpriteRenderer>();

        doorCollider = door.GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sticker"))
        {
            buttonRenderer.sprite = pressedButton;
            doorRenderer.sprite = openedDoor;

            doorCollider.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Sticker"))
        {
            buttonRenderer.sprite = unpressedButton;
            doorRenderer.sprite = closedDoor;

            doorCollider.enabled = true;
        }
    }
}
