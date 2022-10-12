using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersecutionState : State
{
    public GoBackToPatrolState goBackToPatrolState;

    public bool playerLost;

    public override State RunCurrentSate()
    {
        if (playerLost)
        {
            return goBackToPatrolState;
        }
        else
        {
            return this;
        }
    }
}
