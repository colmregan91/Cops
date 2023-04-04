using System;

public class StateTransition
{
    public Istate _from;
    public Istate _to;
    public Func<bool> _condition;

    public StateTransition(Istate from, Istate to, Func<bool> condition)
    {
        _from = from;
        _to = to;
        _condition = condition;
    }
}