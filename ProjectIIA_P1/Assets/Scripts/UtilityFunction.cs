using UnityEngine;
using System.Collections;
using System;

public class UtilityFunction
{
      //check if it is a terminal state (s) 
    public int evaluate(State s)
    {
        int nPlayerUnits = s.PlayersUnits.Count;
        int nAdversaryUnits = s.AdversaryUnits.Count;


        if (nAdversaryUnits == 0 || nPlayerUnits == 0) {
            return -1;
        }
        return 0;
    }
}
