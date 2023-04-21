using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class PointsPowerup : Powerup
{
    public float pointsToAdd;
    public override void Apply(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.controller.AddToScore(pointsToAdd);
        }
    }
    public override void Remove(PowerupManager target)
    {

    }
}
