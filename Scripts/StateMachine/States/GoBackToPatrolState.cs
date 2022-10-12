using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackToPatrolState : State
{
    public PatrolState patrolState;
    public InvestigateState investigateState;
    public PersecutionState persecutionState;

    public bool patrolPointReached;
    public bool goToInvestigate;
    public bool detectedPlayer;

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
        else if(detectedPlayer)
        {
            return persecutionState;
        }
        else
        {
            return this;
        }
    }
}
