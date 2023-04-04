using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToFinishLine : MonoBehaviour
{
    private Mover mover;

    private void Start()
    {
        mover = GetComponentInParent<Mover>();

        PlayerStateMachine.OnStateChanged += HandleStateChange;

    }
    private void OnDisable()
    {
        PlayerStateMachine.OnStateChanged -= HandleStateChange;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        mover.SetShouldMove(true);
    }

    void HandleStateChange(Istate state)
    {
        if (state is Die)
        {
            mover.resetPlatform();
        }
    }

    
}
