using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Data")]
public class MovementData : ScriptableObject
{
    public float runMaxSpeed;
    public float runAccel;
    public float runDecel;

    public float jumpForce;
    public float fallGravityScale;

    public float coyoteTime;

    public Vector2 groundCheckSize;
    public LayerMask groundLayer;
}
