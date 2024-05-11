// MovementManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementManager : MonoBehaviour
{
    public ObjectData Object;
    private float lastOnGroundTime; // Tambahkan variabel ini jika diperlukan
    private Vector2 groundCheckPos;

    private void Update()
    {
        groundCheckPos = new Vector2(Object.col.bounds.center.x, Object.col.bounds.min.y);

        if (Physics2D.OverlapBox(groundCheckPos, Object.Data.groundCheckSize, 0, Object.Data.groundLayer))
        {
            lastOnGroundTime = Object.Data.coyoteTime; // Perbaiki penulisan variabel
        }

        if (lastOnGroundTime > 0f)
        {
            lastOnGroundTime -= Time.deltaTime;
        }

        Debug.Log(lastOnGroundTime);
    }

    private void FixedUpdate()
    {
        Object.Rb.AddForce(Vector2.right * Object.Data.runMaxSpeed * Input.Move.x);

        if(Input.JumpPressed && lastOnGroundTime > 0f)
        {
            Object.Rb.AddForce(Vector2.up * Object.Data.jumpForce, ForceMode2D.Impulse);
            lastOnGroundTime = 0f;
        }
    }

}