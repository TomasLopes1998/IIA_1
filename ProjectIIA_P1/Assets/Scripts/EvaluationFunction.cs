using UnityEngine;
using System.Collections;
using System;

public class EvaluationFunction
{
    // Do the logic to evaluate the state of the game !
    public float evaluate(State s)
    {

        if (s.IsAttack())
        {
            return 1000;
        }
        else
        {
            return 500;
        }
    }
}
