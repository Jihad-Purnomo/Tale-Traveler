// MovementManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementManager : MonoBehaviour
{
    public ObjectData Object;
    private float lastOnGroundTime;
    private Vector2 groundCheckPos;

    private void Update()
    {
        groundCheckPos = new Vector2(Object.col.bounds.center.x, Object.col.bounds.min.y);

        if (Physics2D.OverlapBox(groundCheckPos, Object.Data.groundCheckSize, 0, Object.Data.groundLayer))
        {
            lastOnGroundTime = Object.Data.coyoteTime;
        }

        if (lastOnGroundTime > 0f)
        {
            lastOnGroundTime -= Time.deltaTime;
        }

        Debug.Log(lastOnGroundTime);
    }

    private void FixedUpdate()
    {
        // Calculate target speed and acceleration rate
        float targetSpeed = Input.Move.x * Object.Data.runMaxSpeed;
        float accelRate;

        if (lastOnGroundTime > 0)
        {
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? Object.Data.runAccel : Object.Data.runDecel;
        }
        else
        {
            accelRate = Object.Data.runDecel;
        }

        // Apply acceleration
        Object.Rb.AddForce(Vector2.right * targetSpeed * accelRate);

        // Check for jump input
        if (Input.JumpPressed && lastOnGroundTime > 0f)
        {
            // Apply jump force
            Object.Rb.AddForce(Vector2.up * Object.Data.jumpForce, ForceMode2D.Impulse);
            lastOnGroundTime = 0f;
        }

        // Apply gravity when falling
        if (Object.Rb.velocity.y < 0)
        {
            Object.Rb.gravityScale = Object.Data.fallGravityScale * Object.Data.fallGravityMult;
        }
        else
        {
            Object.Rb.gravityScale = Object.Data.gravityScale;
        }
    }   
}