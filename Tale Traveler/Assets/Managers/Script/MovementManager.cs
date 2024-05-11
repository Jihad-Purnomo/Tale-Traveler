// MovementManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementManager : MonoBehaviour
{
    public Rigidbody2D rb;
    public MovementData Data;
    private Vector2 _groundCheckPos; // Tambahkan inisialisasi posisi
    private Vector2 _groundCheckSize; // Tambahkan inisialisasi ukuran
    private float lastOnGroundTime; // Tambahkan variabel ini jika diperlukan

    private void Update()
    {
        if (Physics2D.OverlapBox(_groundCheckPos, _groundCheckSize, 0, Data.groundLayer))
        {
            lastOnGroundTime = Data.coyoteTime; // Perbaiki penulisan variabel
        }
    }

    private void FixedUpdate()
    {
        // Mengambil input gerakan dari kelas Input
        Vector2 moveInput = Input.Move;

        // Melakukan gerakan horizontal
        rb.velocity = new Vector2(moveInput.x * Data.runMaxSpeed, rb.velocity.y);

        // Melakukan lompat jika tombol lompat ditekan dan pemain berada di tanah atau masih dalam Coyote Time
        if (Input.JumpPressed && (IsGrounded() || Time.time < (Time.fixedTime + Data.coyoteTime)))
        {
            rb.AddForce(Vector2.up * Data.jumpForce, ForceMode2D.Impulse);
        }

        // Mengubah rigidbody.gravityScale saat jatuh
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = Data.fallGravityScale;
        }
        else
        {
            rb.gravityScale = 1f; // Kembalikan gravityScale ke nilai default jika tidak sedang jatuh
        }
    }

    private bool IsGrounded()
    {
        // Implementasi logika pengecekan apakah pemain berada di tanah
        return false; // Misalnya, return hasil pengecekan menggunakan raycast atau overlap sphere
    }
}