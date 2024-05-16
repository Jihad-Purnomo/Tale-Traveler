using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement Data")]
public class MovementData : ScriptableObject
{
    [Header("Horizontal")]
    public float runMaxSpeed;
    public float runAccel;
    public float runDecel;

    [Space(5)]
    public float airMaxSpeed;
    public float airAccel;
    public float airDecel;

    [Space(15)]
    [Header("Gravity")]
    public float jumpForce;
    public float gravityScale;
    public float fallGravityMult;

    [Space(15)]
    [Header("Assist")]
    public float inputBuffer;
    public float coyoteTime;

    [Space(5)]
    public Vector2 groundCheckSize;
    public LayerMask groundLayer;
}
