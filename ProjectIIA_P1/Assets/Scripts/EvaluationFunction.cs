using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class EvaluationFunction
{
    // Do the logic to evaluate the state of the game !
    public float evaluate(State s)
    {
        List<Unit> attackeble = s.AdversaryUnits();

        if (attackeble.Count>0)
        {
            return 1000;
        }
    }
