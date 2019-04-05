using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalFuncV3 : EvaluationFunction
{

    public override double evaluate(State s) {
        if (s.getPlayerUnitsHp() >= s.getAdversaryUnitsHp())
        {
            if (s.getAdversaryUnitsHp() <= 0)
            {
                return double.PositiveInfinity;
            }
            else if (doesAnyoneDies(s.AdversaryUnits))
            {
                return 1000;
            }
            else if (s.getAdversaryUnitsHp() < getTeamMaxHp(s.AdversaryUnits))
            {
                return 1000;
            }
            else {
                return 0;
            }
        }
        else {
            if (s.getPlayerUnitsHp() == 0)
            {
                return 0;
            }
            else if (doesAnyoneDies(s.PlayersUnits)) {
                return 0;
            }
            else if (s.getAdversaryUnitsHp() == 0)
            {
                return double.PositiveInfinity;
            }
            else if (hpBonus(s.PlayersUnits) != 0)
            {-
                return hpBonus(s.PlayersUnits);
            }
            else if (attackBonus(s.PlayersUnits) != 0)
            {
                return attackBonus(s.PlayersUnits);
            }
            else
            {
                return 0;
            }
            }
        }

    public bool doesAnyoneDies(List<Unit> unitList) {
        foreach (Unit unit in unitList) {
            if (unit.IsDead()) {
                return true;
            }
        } 
        return false;
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

    public double hpBonus(List<Unit> listUnits) {
        double hpBonus = 0;
        foreach (Unit unit in listUnits)
        {
            hpBonus += unit.hpbonus;
        }
        return hpBonus;
    }

    public double attackBonus(List<Unit> listUnits) {
        double attackBonus = 0;
        foreach (Unit unit in listUnits)
        {
            attackBonus += unit.attackbonus;
        }
        return attackBonus;
    }
}
