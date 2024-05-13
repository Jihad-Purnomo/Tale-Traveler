// MovementManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovementManager : MonoBehaviour
{
    public ObjectData Object;
    public float lastOnGroundTime { get; private set; }
    public float lastPressedJump { get; private set; }
    private Vector2 groundCheckPos;
    private MovementState currentState;

    public MovementIdle idleState { get; private set; }
    public MovementJumping jumpingState { get; private set; }
    public MovementFalling fallState { get; private set; }
    public MovementRunning runningState { get; private set; }

    private void Awake()
    {
        idleState = new MovementIdle(this);
        jumpingState = new MovementJumping(this);
        fallState = new MovementFalling(this);
        runningState = new MovementRunning(this);
    }

    private void Start()
    {
        currentState = fallState;
    }

    private void Update()
    {
        currentState.UpdateFrame();

        groundCheckPos = new Vector2(Object.col.bounds.center.x, Object.col.bounds.min.y);

        if (Physics2D.OverlapBox(groundCheckPos, Object.Data.groundCheckSize, 0, Object.Data.groundLayer))
        {
            lastOnGroundTime = Object.Data.coyoteTime;
        }

        if (Input.JumpPressed)
        {
            lastPressedJump = Object.Data.inputBuffer;
        }

        if (lastOnGroundTime > 0f)
        {
            lastOnGroundTime -= Time.deltaTime;
        }

        if (lastPressedJump > 0f)
        {
            lastPressedJump -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        currentState.UpdatePhysics();
    }   

    public void ChangeState(MovementState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void ResetJumpTime()
    {
        lastPressedJump = 0f;
    }

    public void ResetGroundTime()
    {
        lastOnGroundTime = 0f;
    }

    public void SetGravityScale(float scale)
    {
        Object.Rb.gravityScale = scale;
    }

    public void HorizontalMovement(float targetSpeed, float accelRate)
    {
        float speedDif = Input.Move.x * targetSpeed - Object.Rb.velocity.x;
        Object.Rb.AddForce(Vector2.right * speedDif * accelRate);
    }
}