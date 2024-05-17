using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementIdle : MovementState
{
    public MovementIdle(Movement movement) : base(movement)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void UpdateFrame()
    {
        base.UpdateFrame();

        if (Input.Move.x != 0f)
        {
            movement.ChangeState(movement.runningState);
        }

        if (movement.lastPressedJump > 0f)
        {
            movement.ChangeState(movement.jumpingState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();


        movement.Horizontal(0f, movement.Object.Data.runDecel);
    }
}
