using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISniper : AIController
{ 
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    } 

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    //FSM for sniper AI varient
    public override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                if (!IsHasTarget())
                {
                    
                    ChangeState(AIState.ChooseTarget);
                }
                DoIdleState();
                if (target != null)
                {
                    if (IsDistanceLessThan(target, 40))
                    {

                        ChangeState(AIState.Chase);
                    }
                }
                break;
            case AIState.Chase:
                if (!IsHasTarget())
                {
                    
                    ChangeState(AIState.ChooseTarget);

                }
                DoAttackState();
                if (target != null)
                {
                    if (!IsDistanceLessThan(target, 40))
                    {

                        ChangeState(AIState.Idle);
                    }
                }
               
                break;
       
            
            case AIState.ChooseTarget:
                DoChooseTargetState();
                
                ChangeState(AIState.Idle);

                break;
            

        }

    }
}
