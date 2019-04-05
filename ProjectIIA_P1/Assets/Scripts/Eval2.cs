using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eval2 : EvaluationFunction
{


        public override double evaluate(State s){
            if (getTeamMaxHp(s.AdversaryUnits)>s.getAdversaryUnitsHp()) {
                    return 10000;
            }
            else {
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
