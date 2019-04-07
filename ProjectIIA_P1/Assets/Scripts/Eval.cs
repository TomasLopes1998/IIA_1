using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eval : EvaluationFunction
{
    
    public override double evaluate(State s)
    {
        //se a nossa vida maior ou igual à equipa adversária
        if (s.getPlayerUnitsHp() >= getTeamMaxHp(s.PlayersUnits))
        {
            return 200;
        }
        //se se a nossa vida está igual ao maximo
        else if (s.getPlayerUnitsHp() >= getTeamMaxHp(s.PlayersUnits) && s.PlayersUnits.Count>0)
        {
            return 100;
        }
        else if (s.getPlayerUnitsHp() - s.getAdversaryUnitsHp() > 0)
        {
            return s.getPlayerUnitsHp() - s.getAdversaryUnitsHp();
        }
        else if (s.AdversaryUnits.Count < s.PlayersUnits.Count)
        {
            return s.PlayersUnits.Count * 10;
        }
        else
        {
            return 0;
        }
    }


    public double getTeamMaxHp(List<Unit> listUnits)
    {
        double maxHpSum = 0;
        foreach (Unit unit in listUnits)
        {
            maxHpSum += unit.maxHp;
        }
        return maxHpSum;
    }


}
