using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState
{
    protected MovementManager movement;

    public MovementState(MovementManager movement)
    {
        this.movement = movement;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateFrame() { }
    public virtual void UpdatePhysics() { }
}
