using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EvaluationFunction
{
    // Do the logic to evaluate the state of the game !
    public double evaluate(State s)
    {
         if (s.AdversaryUnits.Count < s.PlayersUnits.Count || s.IsAttack())
         {
             return 10000;
         }
        else {
            return -10000;
        }
    }   
}
