using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public InvestigateState investigateState;

    public bool goToInvestigate;

    public override State RunCurrentSate()
    {

        if (goToInvestigate)
        {
            return investigateState;
        }
        else
        {
            return this;
        }
    }
}
