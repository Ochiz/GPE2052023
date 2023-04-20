using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class DamagePowerup : Powerup
{
    public float damageToAdd;
    public override void Apply(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.damageDone = targetPawn.damageDone + damageToAdd;
        }
    }
    public override void Remove(PowerupManager target)
    {
        Pawn targetPawn = target.GetComponent<Pawn>();
        if (targetPawn != null)
        {
            targetPawn.damageDone = targetPawn.damageDone - damageToAdd;
        }
    }
}
