using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Guard, Chase, Flee, Patrol, Attack, Scan, BackToPost, ChooseTarget };

    public AIState currentState;

    public float hearingDistance;

    public float fieldOfView;

    private float lastStateChangeTime;

    public GameObject target;

    public float fleeDistance;

    public Transform[] waypoints;

    public float waypointStopDistance;

    private int currentWaypoint = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.aiPlayers != null)
            {
                GameManager.instance.aiPlayers.Add(this);
            }
        }
        ChangeState(AIState.Idle);
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        MakeDecisions();
        base.Update();
    }

    public void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                if (!IsHasTarget())
                {
                    TargetPlayerOne();
                }
                DoIdleState();
                if (IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                DoAttackState();
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Idle);
                }
                if (pawn.health.currentHealth <= 30)
                {
                    ChangeState(AIState.Flee);
                }
                break;
            case AIState.Flee:
                DoFleeState();
                if (!IsDistanceLessThan(target, 10))
                {
                    ChangeState(AIState.Idle);
                }
                break;

        }

    }
    
    public override void ProcessInputs()
    {
        //nothing here
    }
    
    public void Shoot()
    {
        pawn.Shoot();
    }

    public virtual void ChangeState(AIState newState)
    {
        currentState = newState;
        lastStateChangeTime = Time.time;
    }
    
    protected bool IsHasTarget()
    {
        return (target != null);
    }

    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void Seek(GameObject target)
    {
        pawn.RotateTowards(target.transform.position);
        pawn.MoveForward(false);
    } 
    
    public void Seek(Vector3 targetPosition)
    {
        pawn.RotateTowards(targetPosition);
        pawn.MoveForward(false);
    }

    public void Seek(Pawn targetPawn)
    {
        Seek(targetPawn.transform);
    }

    public void Seek(Transform targetTransform)
    {
        Seek(targetTransform.position);
    }

    public void Seek(Controller targetController)
    {
        Seek(targetController.pawn);
    }

    public void DoAttackState()
    {
        Seek(target);
        Shoot();
    }

    protected virtual void DoIdleState()
    {
        //nothing
    }

    protected void Flee()
    {
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position); // gets distance between target and AI
        float percentOfFleeDistance = targetDistance / fleeDistance; //determins what percent of the flee distance is the current distance away
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance); //clamps flee distance into number between 0 and 1 for percentage math
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance; // flips flee distance 
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        Vector3 fleeVector = vectorAwayFromTarget.normalized * (fleeDistance * flippedPercentOfFleeDistance);
        Seek(pawn.transform.position + fleeVector.normalized);
    }

    public void DoFleeState()
    {
        Flee();
    }

    protected void Patrol()
    {
        if (waypoints.Length > currentWaypoint)
        {
            Seek(waypoints[currentWaypoint]);
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        currentWaypoint = 0;
    }

    public void DoPatrolState()
    {
        Patrol();
    }

    public void TargetPlayerOne()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.players != null)
            {
                if (GameManager.instance.players.Count > 0)
                {
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }

    protected void TargetNearestTank()
    {
        Pawn[] allTanks = FindObjectsOfType<Pawn>();
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        foreach (Pawn tank in allTanks)
        {
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }
        target = closestTank.gameObject;
    }

    protected void TargetLowestHealthTank()
    {
        Pawn[] allTanks = FindObjectsOfType<Pawn>();
        Pawn lowestHPTank = allTanks[0];
        float lowestHP = lowestHPTank.health.currentHealth;

        foreach (Pawn tank in allTanks)
        {
            if (tank.health.currentHealth <= lowestHP)
            {
                lowestHPTank = tank;
                lowestHP = lowestHPTank.health.currentHealth;
            }
        }
        target = lowestHPTank.gameObject;
    }

    protected void ChooseTarget()
    {
        TargetPlayerOne();
    }

    public void DoChooseTargetState()
    {
        ChooseTarget();
    }

    public bool CanHear(GameObject target)
    {
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        if (noiseMaker == null)
        {
            return false;
        }
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CanSee(GameObject target)
    {
        Vector3 angleToTargetVector = target.transform.position - transform.position;
        float angleToTarget = Vector3.Angle(angleToTargetVector, pawn.transform.forward);
        if (angleToTarget < fieldOfView)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.aiPlayers != null)
            {
                GameManager.instance.aiPlayers.Remove(this);
            }
        }
    }

}
