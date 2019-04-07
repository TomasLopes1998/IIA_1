using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eval2 : EvaluationFunction
{


    public override double evaluate(State s)
    {
        //se o numero de mortos for igual ao numero de adversários!
        if (nDeadsInTeam(s.AdversaryUnits) == s.AdversaryUnits.Count)
        {
            return 10000;
        }
        else
        {
            return 0;
        }
    }


    public int nDeadsInTeam(List<Unit> unitList)
    {
        int nDeads = 0;
        foreach (Unit unit in unitList)
        {
            if (unit.IsDead())
            {
                nDeads++;
            }
        }
        return nDeads;
    }
}
