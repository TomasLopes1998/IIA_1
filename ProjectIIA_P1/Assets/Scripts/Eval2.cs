using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eval2 : EvaluationFunction
{


    public override double evaluate(State s)
    {
        //se o numero de mortos for igual ao numero de adversários!
        if (s.AdversaryUnits.Count == 0)
        {
            return 10000;
        }
        else
        {
            return 0;
        }
    }
}
