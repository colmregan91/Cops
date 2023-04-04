using UnityEngine;
using System;
using System.Collections.Generic;
public class Die : Istate
{
    private Animator anim;

    public Die (Animator _anim)
    {
        anim = _anim; // players animator


    }
    
    public void OnEnter()
    {
        anim.SetTrigger("Die"); // play die animation

        GameManager.Instance.LoseLife(); // deduct a life

        GameManager.Instance.InvokeRespawnOrGameover(); // check if respawn or gameover is required
    }
    public void OnExit()
    {
        anim.SetTrigger("Respawn"); // go from die to move in animation state
    }

    public void OnUpdate()
    {
    }
}

