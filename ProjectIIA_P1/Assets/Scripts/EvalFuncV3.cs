using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvalFuncV3 : EvaluationFunction
{

    public override double evaluate(State s)
    {
        //Verifica se a nossa vida é maior ou igual à nossa 
        if (s.getPlayerUnitsHp() >= s.getAdversaryUnitsHp())
        {
            //verificamos se a vida dos adversários é menor ou igual a 0 (se o jogo acabou)
            if (s.getAdversaryUnitsHp() <= 0)
            {
                return 10000000;
            }
            //verificamos o numero de mortos 
            else if (s.AdversaryUnits.Count > 0)
            {
                return 100000 / s.AdversaryUnits.Count;
            }
            //se perdeu vida
            else if (s.getAdversaryUnitsHp() < getTeamMaxHp(s.AdversaryUnits))
            {
                return 1000;
            }
            //Verifica os bonus
            else if (attackBonus(s.PlayersUnits) != 0)
            {
                return attackBonus(s.PlayersUnits);
            }
            else if (hpBonus(s.PlayersUnits) != 0)
            {
                return hpBonus(s.PlayersUnits);
            }
            else
            {
                return 0;
            }
        }
        //Nossa vida menor!
        else
        {   
            //we dead
            if (s.PlayersUnits.Count == 0 ) {
                return 0;
            }
            //se a nossa vida for menor, mas está no maximo e o nosso numero de unidades é maior que 0
            else if (s.getPlayerUnitsHp() >= getTeamMaxHp(s.PlayersUnits) && s.PlayersUnits.Count>0) {
                return 100000;
            }
            //verifica os bonus
            else if (hpBonus(s.PlayersUnits) != 0)
            {
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
    

    //vida maxima de uma equipa
    public double getTeamMaxHp(List<Unit> listUnits)
    {
        double maxHpSum = 0;
        foreach (Unit unit in listUnits)
        {
            maxHpSum += unit.maxHp;
        }
        return maxHpSum;
    }
    //verifica bonus de vida 
    public double hpBonus(List<Unit> listUnits)
    {
        double hpBonus = 0;
        foreach (Unit unit in listUnits)
        {
            hpBonus += unit.hpbonus;
        }
        return hpBonus;
    }
    //verifica bonus de ataque  
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