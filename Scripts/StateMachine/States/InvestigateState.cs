using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigateState : State
{
    //public GoBackToPatrolState goBackToPatrolState;
    public PatrolState patrolState;
    public PersecutionState persecutionState;

    public bool playerLost;
    public bool detectPlayer;

    public override State RunCurrentSate()
    {
        if (playerLost)
        {
            return patrolState;
        }
        else if (detectPlayer)
        {
            return persecutionState;

        }
        else
        {
            return this;
        }
    }
}
