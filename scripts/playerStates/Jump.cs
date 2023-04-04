using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : Istate
{
    public float jumpForce = 1200;
    private Rigidbody2D rb;
    private Animator anim;
    private Move move;


    public Jump(Rigidbody2D _rb, Animator _anim, Move _move) // constructor
    {
        rb = _rb;
        anim = _anim;
        move = _move;
    }

    public void OnEnter()
    {
        anim.SetTrigger("Jump");
        soundManager.Instance.PlayJumpClip(); // play jump sound
        anim.SetBool("grounded", false); // change to jump anim
        rb.AddForce(new Vector3(move.horizontal, jumpForce, 0)); // add force to players rigidbody in given direction (x axis) and force (Y axis)


    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        move.MovePlayer();
    }
}

