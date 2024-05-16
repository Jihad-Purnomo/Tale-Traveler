// MovementManager.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public ObjectData Object { get; private set; }

    public float lastOnGroundTime { get; private set; }
    public float lastPressedJump { get; private set; }

    private Vector2 groundCheckPos;
    private MovementState currentState;

    public MovementIdle idleState { get; private set; }
    public MovementJumping jumpingState { get; private set; }
    public MovementFalling fallingState { get; private set; }
    public MovementRunning runningState { get; private set; }

    private void Awake()
    {
        idleState = new MovementIdle(this, Object);
        jumpingState = new MovementJumping(this, Object);
        fallingState = new MovementFalling(this, Object);
        runningState = new MovementRunning(this, Object);
    }

    private void Start()
    {
        InitializeState(idleState);
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

        Countdown(lastOnGroundTime);
        Countdown(lastPressedJump);
    }

    private void FixedUpdate()
    {
        currentState.UpdatePhysics();
    }

    public void InitializeState(MovementState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(MovementState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void Countdown(float timer)
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
        }
    }

    public void ResetJumpTime()
    {
        lastPressedJump = 0f;
    }

    public void ResetGroundTIme()
    {
        lastOnGroundTime = 0f;
    }

    public void SetGravityScale(float scale)
    {
        Object.Rb.gravityScale = scale;
    }

    public void ResetUpMomentum()
    {
        Object.Rb.velocity = new Vector2(Object.Rb.velocity.x, 0);
    }

    public void Horizontal(float targetSpeed, float accelRate)
    {
        float speedDif = Input.Move.x * targetSpeed - Object.Rb.velocity.x;
        Object.Rb.AddForce(Vector2.right * speedDif * accelRate);
    }

    public void ActivateObject(ObjectData objectData)
    {
        Object = objectData;
        SetGravityScale(Object.Data.gravityScale);
    }
}