using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState
{
    protected Movement movement;
    protected ObjectData objectData;

    public MovementState(Movement movement)
    {
        this.movement = movement;
    }

    public MovementState(Movement movement, ObjectData objectData)
    {
        this.movement = movement;
        this.objectData = objectData;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void UpdateFrame() { }
    public virtual void UpdatePhysics() { }
}
