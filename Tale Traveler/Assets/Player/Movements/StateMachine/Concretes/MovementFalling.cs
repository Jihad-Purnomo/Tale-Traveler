using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFalling : MovementState
{
    public MovementFalling(Movement movement) : base(movement)
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
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        if (movement.lastOnGroundTime > 0f)
        {
            AudioManager.Inst.PlayAudio(movement.Object.landingSound, movement.Object.transform);

            movement.ChangeState(movement.idleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
