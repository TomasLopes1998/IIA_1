﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepCopyExtensions;

public class MinMaxAlgorithm1_0 : MoveMaker
{
    public EvaluationFunction evaluator;
    private UtilityFunction utilityfunc;
    public int depth = 0;
    private PlayerController MaxPlayer;
    private PlayerController MinPlayer;
    public int maxDepth = 0;
    public State tempState = null;
    public State nextMove = null;

    public MinMaxAlgorithm1_0(PlayerController MaxPlayer, EvaluationFunction eval, UtilityFunction utilf, PlayerController MinPlayer)
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
        this.MaxPlayer.ExpandedNodes = 0;
        // Creates initial state
        State newState = new State(this.MaxPlayer, this.MinPlayer);
        // Call the MinMax implementation
        State bestMove = MinMax(newState);
        // returning the actual state. You should modify this
        return bestMove;
    }

    public State MinMax(State actual)
    { 
        //Para determinar a profundidade maxima de maneira a expandir todos os nós horizontalmente
        maxDepth = 1;
        double v = valMax(actual);
        while (this.MaxPlayer.ExpandedNodes < this.MaxPlayer.MaximumNodesToExpand)
        {
            nextMove = tempState;
            maxDepth++;
            v = valMax(actual);
        }
        return nextMove;
    }


    public double valMax(State estado)
    {
        //muda a perspectiva de jogo (o Código fornecido já verifica se o estado é raiz)
        State changePers = new State(estado);
        //se é estado terminal
        if (utilityfunc.evaluate(estado) < 0 || this.MaxPlayer.ExpandedNodes > this.MaxPlayer.MaximumNodesToExpand  || estado.depth >= maxDepth)
        {
            return evaluator.evaluate(estado);
        }

        double v = Double.NegativeInfinity;
        //verifica as hipoteses
        List<State> possibleStates = this.GeneratePossibleStates(estado);
        foreach (State state in possibleStates)
        {
            double v_min = valMin(state);
            //devolve o máximo!
            if (v_min>=v) {
                v = v_min;
                //se for raiz atualiza o estado
                if (estado.isRoot) {
                    tempState = state;
                }
            }
        }
        return v;
    }

    public double valMin(State estado)
    {
        //muda perspectiva de jogo
        State changePers = new State(estado);
        //verifica se é estado Final
        if (utilityfunc.evaluate(estado) < 0 || this.MaxPlayer.ExpandedNodes > this.MaxPlayer.MaximumNodesToExpand ||  estado.depth >=maxDepth)
        {
            return evaluator.evaluate(estado);
        }
        double v = Double.PositiveInfinity;
        //gera estados
        List<State> possibleStates = this.GeneratePossibleStates(estado);
        foreach (State state in possibleStates)
        {
            double vMax = valMax(state);
            if (v<=vMax) {
                v = vMax;
            }
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

        state.board[attacked.x, attacked.y] = attacked;
        int index = state.AdversaryUnits.IndexOf(attacked);
        state.AdversaryUnits[index] = attacked;



        if (attacked.hp <= 0)
        {
            //Board update by killing the unit!
            state.board[attacked.x, attacked.y] = null;
            index = state.AdversaryUnits.IndexOf(attacked);
            state.AdversaryUnits.RemoveAt(index);

        }
        state.isMove = false;
        state.isAttack = true;

        return state;

    }
}