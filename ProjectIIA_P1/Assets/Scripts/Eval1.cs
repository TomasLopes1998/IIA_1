using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eval1 : EvaluationFunction
{
    public override double evaluate(State s)
    {
        if (s.getPlayerUnitsHp() - s.getAdversaryUnitsHp() > 0 && s.AdversaryUnits.Count < s.PlayersUnits.Count)
        {
            return 50;
        }
        else if (s.AdversaryUnits.Count < s.PlayersUnits.Count) {
            return 30;
        }
        else if (s.getPlayerUnitsHp() - s.getAdversaryUnitsHp() > 0) {
            return 20;
        }
        else {
            return 10;
        }
    }
}
