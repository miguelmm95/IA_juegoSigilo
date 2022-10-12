using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public InvestigateState investigateState;
    public PersecutionState persecutionState;

    public bool goToInvestigate;
    public bool detectPlayer;

    public override State RunCurrentSate()
    {

        if (goToInvestigate)
        {
            return investigateState;
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
