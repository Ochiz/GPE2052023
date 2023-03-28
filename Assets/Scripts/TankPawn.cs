using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Start();
    }
    //functions to move tank below
    public override void MoveForward(bool sprint)
    {
        if (sprint)
        {
            mover.Move(transform.forward, sprintSpeed);
        }
        else
        {
            mover.Move(transform.forward, moveSpeed);
        }

    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }
}
