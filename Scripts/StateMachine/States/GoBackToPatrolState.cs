using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToPatrolState : State
{
    public PatrolState patrolState;
    public InvestigateState investigateState;

    public bool patrolPointReached;
    public bool goToInvestigate;

    public override State RunCurrentSate()
    {
        if (patrolPointReached)
        {
            return patrolState;
        }
        else if (goToInvestigate)
        {
            return investigateState;
        }
        else
        {
            return this;
        }
    }
}
