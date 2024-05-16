using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFalling : MovementState
{
    public MovementFalling(Movement movement, ObjectData objectData) : base(movement, objectData)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        movement.SetGravityScale(movement.Object.Data.gravityScale * movement.Object.Data.fallGravityMult);
    }

    public override void ExitState()
    {
        base.ExitState();

        movement.SetGravityScale(movement.Object.Data.gravityScale);
        AudioManager.Inst.PlayAudio(objectData.landingSound, objectData.transform);
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        if (movement.lastOnGroundTime > 0f)
        {
            movement.ChangeState(movement.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
