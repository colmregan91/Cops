using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSetter : MonoBehaviour
{

    public enum animType // controls what type of animation will play
    {
        None,
        walk,
        run
    }

    public animType AnimType; // set in inspector
    private Animator anim;
    private Collisions cols;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        cols = GetComponentInChildren<Collisions>();

        CheckAnimType();
    }

    void CheckAnimType()
    {

        if (AnimType == animType.walk) // if walk is chosen set the animation 
        {
            anim.SetBool("walk", true);
        }
        else if (AnimType == animType.run)// if run is chosen set the animation 
        {
            anim.SetBool("run", true);
        }
    }

    void ResetAnim()
    {
        anim.SetBool("run", false);
        anim.SetBool("walk", false);
    }
    private void OnEnable()
    {
        PlayerStateMachine.OnStateChanged += onPlayerStateChange;
    }
    private void OnDisable()
    {
        PlayerStateMachine.OnStateChanged -= onPlayerStateChange;
    }


    void onPlayerStateChange(Istate newState)
    {
        if (newState is Jump) return;
        if (cols.dead) return;

        if (newState is Move)
        {
            CheckAnimType();
        }

        if (newState is Die)
        {
            ResetAnim();
        }


    }
}


