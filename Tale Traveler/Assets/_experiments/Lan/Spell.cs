using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    private Vector2 _direction;
    [SerializeField] float _speed;
    private MovementManager Movement;

    private void Awake()
    {
        Movement = FindObjectOfType<MovementManager>();
    }

    private void OnEnable()
    {
        _direction = new Vector2(1, Input.Move.y);
    }

    private void Update()
    {
        transform.Translate(_direction * Time.deltaTime * _speed);
    }

    private void OnDisable()
    {
        transform.position = transform.parent.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Sticker"))
        {
            Movement.ActivateObject(collision.GetComponent<ObjectData>());
        }
    }
}
