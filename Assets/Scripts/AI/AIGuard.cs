using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGuard : AIController
{
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    //FSM for Guard AI Varient
    public override void MakeDecisions()
    {
        switch (currentState)
        {
            
            case AIState.Chase:
                if (!IsHasTarget())
                {
                    
                    ChangeState(AIState.ChooseTarget);

                }
                DoAttackState();
                
                break;
            
            
            case AIState.ChooseTarget:
                DoChooseTargetState();
                
                ChangeState(AIState.Guard);

                break;
            case AIState.Guard:
                if (!IsHasTarget())
                {
                    
                    ChangeState(AIState.ChooseTarget);
                }
                if (target != null)
                {
                    if(CanHear(target))
                    {
                        ChangeState(AIState.Chase);
                    }
                    if (CanSee(target))
                    {
                        
                        ChangeState(AIState.Chase);
                    }
                }
                break;

        }

    }
}
