using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaddersMovement : MonoBehaviour
{
    //aku gak tau 3 yang dibawah ini bisa di ganti apa biar labih efesien sama kode kita
    public float speed;
    private float InputVertical;
    private Rigidbody2D rb;

    public float distance;
    public LayerMask WhatIsLadder;
    private bool isClimbing;

    private void FixedUpdate()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, WhatIsLadder);

        if(hitInfo.collider != null)
        {
            // error dibagian input.getkeydown
            //if(Input.GetKeyDown(KeyCode.UpArrow))
            //{
            //    isClimbing = true;
            //}
            //else
            //{
            //    isClimbing = false;
            //}
            //if (isClimbing == true)
            //{
            //    // gak tau bisa di ganti sama apa ini input vertical
            //    //inputVertical = Input.GetAxisRaw("Vertical");
            //    rb.velocity = new Vector2(rb.velocity.x, InputVertical * speed);
            //    rb.gravityScale = 0;
            //}
            //else
            //{
            //    rb.gravityScale = 5;
            //}
        }
    }
}