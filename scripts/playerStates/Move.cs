using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Istate
{
    private groundDetection groundDet;
    private bool jumpRequest;
    public float horizontal;
    public float moveSpeed = 10;
    private Animator anim;
    private SpriteRenderer spRenderer;
    public Transform playerTransform;
    public bool canMove = true;

    public Move(groundDetection _gDetection, Transform _playerTransform, Animator _anim, SpriteRenderer _renderer) // constructor
    {
        groundDet = _gDetection; // set all values for the class
        playerTransform = _playerTransform;
        anim = _anim;
        spRenderer = _renderer;
    }


    public bool GetJumpRequest()
    {
        return jumpRequest;
    }


    public void OnEnter()
    {

        anim.SetBool("Move", true); // set moving bool on animator to true

    }

    public void OnExit()
    {
        jumpRequest = false; // set jump request to false
    }

    public void MovePlayer()
    {
        horizontal = Input.GetAxis("Horizontal"); // go between idle and move animation depending on horizontal value

        Vector3 move = new Vector3(horizontal, 0);
        playerTransform.position += move * Time.deltaTime * moveSpeed; // move the player to this new position



        if (horizontal != 0)
        {
            spRenderer.flipX = horizontal < 0; // flip the image horizontally depending on which direction the player is going
        }
    }

    public void OnUpdate()
    {
        if (!canMove) return;  // dont do anything if can move is false

        MovePlayer(); // move the player

        anim.SetBool("Move", Mathf.Abs(horizontal) > 0.01f); // go into idle 

        if (Input.GetKeyDown(KeyCode.Space)) // if the player presses space
        {
            if (groundDet.GetIsGrounded() && anim.GetBool("grounded") == true) // if player is grounded and anim is grounded too
            {
                groundDet.isGrounded = false; // set is grounded to false
                jumpRequest = true; // make the player jump
            }
        }
    }
}

