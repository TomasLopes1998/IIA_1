using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepCopyExtensions;
using Random = System.Random;

public class MinMaxAlgorithm : MoveMaker
{
    public EvaluationFunction evaluator;
    private UtilityFunction utilityfunc;
    private PlayerController MaxPlayer;
    private PlayerController MinPlayer;
    public int maxDepth = 4;

    public MinMaxAlgorithm(PlayerController MaxPlayer, EvaluationFunction eval, UtilityFunction utilf, PlayerController MinPlayer)
    {
        this.MaxPlayer = MaxPlayer;
        this.MinPlayer = MinPlayer;
        this.evaluator = eval;
        this.utilityfunc = utilf;
    }

    public override State MakeMove()
    {
        // The move is decided by the selected state
        return GenerateNewState();
    }

    private State GenerateNewState()
    {
        // Creates initial state
        State newState = new State(this.MaxPlayer, this.MinPlayer);
        // Call the MinMax implementation
        State bestMove = MinMax(newState);
        // returning the actual state. You should modify this
        //should it return the best move? 
        return bestMove;
    }

    public State MinMax(State actual)
    {
        List<State> listStates = new List<State>();
        double v = valMax(actual);
        List<State> possibleStates = GeneratePossibleStates(actual);
        foreach (State state in possibleStates) {
            if (evaluator.evaluate(state) == v) {
                listStates.Add(state);
            }
        }    
        if (listStates.Count!= 1) {
            Random random = new Random();
            int n = random.Next(0,listStates.Count);
            return listStates[n];
        }
        else
        {
            return listStates[0];
        }
    }


    public double valMax(State estado) {
        //Debug.Log("DepthEstado = " +estado.depth);
        if (utilityfunc.evaluate(estado) < 0 || this.MaxPlayer.ExpandedNodes >  this.MaxPlayer.MaximumNodesToExpand ||  estado.depth > this.maxDepth) {
            return evaluator.evaluate(estado);
        }
        double v = Double.NegativeInfinity;
        List<State> possibleStates = this.GeneratePossibleStates(estado);
        foreach (State state in possibleStates) {
            v = Math.Max(v, valMin(state));
        }
        return v;
    }

    public double valMin(State estado) {
        if (utilityfunc.evaluate(estado) < 0 || this.MaxPlayer.ExpandedNodes > this.MaxPlayer.MaximumNodesToExpand || estado.depth > this.maxDepth) {
            return evaluator.evaluate(estado);
        }
        double v = Double.PositiveInfinity;
        List<State> possibleStates = this.GeneratePossibleStates(estado);
        foreach (State state in possibleStates) {
            v = Math.Min(v, valMax(state));
        }
        return v;
    }

    private List<State> GeneratePossibleStates(State state)
    {
        List<State> states = new List<State>();
        //Generate the possible states available to expand
        foreach (Unit currentUnit in state.PlayersUnits)
        {
            // Movement States
            List<Tile> neighbours = currentUnit.GetFreeNeighbours(state);
            foreach (Tile t in neighbours)
            {
                State newState = new State(state, currentUnit, true);
                newState = MoveUnit(newState, t);
                states.Add(newState);
            }
            // Attack states
            List<Unit> attackOptions = currentUnit.GetAttackable(state, state.AdversaryUnits);
            foreach (Unit t in attackOptions)
            {
                State newState = new State(state, currentUnit, false);
                newState = AttackUnit(newState, t);
                states.Add(newState);
            }

        }

        // YOU SHOULD NOT REMOVE THIS
        // Counts the number of expanded nodes;
        this.MaxPlayer.ExpandedNodes += states.Count;
        //

        return states;
    }

    private State MoveUnit(State state, Tile destination)
    {
        Unit currentUnit = state.unitToPermormAction;
        //First: Update Board
        state.board[(int)destination.gridPosition.x, (int)destination.gridPosition.y] = currentUnit;
        state.board[currentUnit.x, currentUnit.y] = null;
        //Second: Update Players Unit Position
        currentUnit.x = (int)destination.gridPosition.x;
        currentUnit.y = (int)destination.gridPosition.y;
        state.isMove = true;
        state.isAttack = false;
        return state;
    }

    private State AttackUnit(State state, Unit toAttack)
    {
        Unit currentUnit = state.unitToPermormAction;
        Unit attacked = toAttack.DeepCopyByExpressionTree();

        Tuple<float, float> currentUnitBonus = currentUnit.GetBonus(state.board, state.PlayersUnits);
        Tuple<float, float> attackedUnitBonus = attacked.GetBonus(state.board, state.AdversaryUnits);


        attacked.hp += Math.Min(0, (attackedUnitBonus.Item1)) - (currentUnitBonus.Item2 + currentUnit.attack);
        state.unitAttacked = attacked;

        if (attacked.hp <= 0)
        {
            //Board update by killing the unit!
            state.board[attacked.x, attacked.y] = null;
            int index = state.AdversaryUnits.IndexOf(attacked);
            state.AdversaryUnits.RemoveAt(index);

        }
        state.isMove = false;
        state.isAttack = true;

        return state;

    }

}