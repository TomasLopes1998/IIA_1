using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eval4Units : EvaluationFunction
{
    public override double evaluate(State s)
    {
        int nDeadsPlayer = nDeadsInTeam(s.PlayersUnits);
        int nDeadsAdversary = nDeadsInTeam(s.AdversaryUnits);
        //verifica se a nossa vida é menor que a vida maxima
        if (s.getPlayerUnitsHp()<getTeamMaxHp(s.PlayersUnits)) {
            if (nDeadsAdversary == s.AdversaryUnits.Count) {
                return 10000;
            }
            return 0;
            
        }
        //numero de mortos na equipa adversária maior ou igual que na equipa do jogador
        else if (nDeadsAdversary >= nDeadsPlayer)
        {
            //aumenta dado o numero de mortes na equipa 
            if (nDeadsAdversary > 0)
            {
                return nDeadsAdversary * 1000;
            }
            //verifica as vidas das equipas
            else if (s.getAdversaryUnitsHp() < getTeamMaxHp(s.AdversaryUnits))
            {
                return (getTeamMaxHp(s.AdversaryUnits) - s.getAdversaryUnitsHp());
            }
            //verifica bonus
            else if (hpBonus(s.PlayersUnits) != 0)
            {
                return hpBonus(s.PlayersUnits) + 1000;
            }
            else if (attackBonus(s.PlayersUnits) != 0)
            {
                return attackBonus(s.PlayersUnits) + 1000;
            }
            else
            {
                return 0;
            }
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

    public double getTeamMaxHp(List<Unit> listUnits)
    {
        double maxHpSum = 0;
        foreach (Unit unit in listUnits)
        {
            maxHpSum += unit.maxHp;
        }
        return maxHpSum;
    }

    public double hpBonus(List<Unit> listUnits)
    {
        double hpBonus = 0;
        foreach (Unit unit in listUnits)
        {
            hpBonus += unit.hpbonus;
        }
        return hpBonus;
    }

    public double attackBonus(List<Unit> listUnits)
    {
        double attackBonus = 0;
        foreach (Unit unit in listUnits)
        {
            attackBonus += unit.attackbonus;
        }
        return attackBonus;
    }
}
