using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public Istate _currentState;

    public List<StateTransition> transitions = new List<StateTransition>();

    public Action<Istate> OnStateChanged;

    public void AddTransition(Istate from, Istate to, Func<bool> condition)
    {
        StateTransition newtransition = new StateTransition(from, to, condition);
        transitions.Add(newtransition);

    }

    public void Tick()
    {
        StateTransition transition = checkForTransition();
        if (transition != null)
        {
            SetState(transition._to);
        }

        _currentState.OnUpdate();
    }

    public void SetState(Istate state)
    {
        if (_currentState == state)
        {
            return;
        }
        _currentState?.OnExit();
        _currentState = state;
        _currentState.OnEnter();

        OnStateChanged?.Invoke(_currentState);
    }

    StateTransition checkForTransition()
    {

        foreach (var trans in transitions)
        {

            if (_currentState == trans._from && trans._condition())
            {
                return trans;
            }
        }
        return null;
    }
}
