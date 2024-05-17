using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState
{
    protected Movement movement;

    public MovementState(Movement movement)
    {
        this.movement = movement;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateFrame() { }
    public virtual void UpdatePhysics() { }
}
