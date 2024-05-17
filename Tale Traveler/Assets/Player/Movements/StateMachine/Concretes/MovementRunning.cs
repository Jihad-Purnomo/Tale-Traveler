using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRunning : MovementState
{
    public MovementRunning(Movement movement) : base(movement)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        
        AudioManager.Inst.PlayAudio(movement.Object.runningSound, movement.Object.transform);
    }

    public override void ExitState()
    {
        base.ExitState();

        AudioManager.Inst.StopAudio(movement.Object.runningSound);
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        if(Input.Move.x == 0f)
        {
            movement.ChangeState(movement.idleState);
        }

        if (movement.lastPressedJump > 0f)
        {
            movement.ChangeState(movement.jumpingState);
        }

        if (movement.lastOnGroundTime <= 0f)
        {
            movement.ChangeState(movement.fallingState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();


        movement.Horizontal(movement.Object.Data.runMaxSpeed, movement.Object.Data.runAccel);
    }
}
