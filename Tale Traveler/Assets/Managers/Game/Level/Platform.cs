using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Movement movement;
    private Collider2D coll;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask stickerLayer;

    [SerializeField] private bool passThrough;

    private void Awake()
    {
        movement = FindObjectOfType<Movement>();
        coll = GetComponent<Collider2D>();
    }

    private void Start()
    {
        coll.excludeLayers = playerLayer & stickerLayer;
    }

    private void Update()
    {
        if (passThrough)
        {
            if (movement.groundCheckPos.y > coll.bounds.max.y)
            {
            }
        }
    }
}
