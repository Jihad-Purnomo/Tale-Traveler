using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJumping : MovementState
{
    public MovementJumping(Movement movement, ObjectData objectData) : base(movement, objectData)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        AudioManager.Inst.PlayAudio(objectData.jumpingSound, objectData.transform);

        movement.ResetUpMomentum();
        movement.Object.Rb.AddForce(Vector2.up * movement.Object.Data.jumpForce, ForceMode2D.Impulse);
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        movement.ResetGroundTIme();
        movement.ResetJumpTime();

        if (movement.Object.Rb.velocity.y <= 0f)
        {
            movement.ChangeState(movement.fallingState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        float accel = Input.Move.x != 0f ? movement.Object.Data.airAccel : movement.Object.Data.airDecel;
        movement.Horizontal(movement.Object.Data.airMaxSpeed, accel);
    }
}
