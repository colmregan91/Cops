using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Transform MovingObject;
    public float percComplete;
    public float direction = 1;
    public float speed;
    private SpriteRenderer sp;
    private bool shouldFlip;
    public bool shouldMove = true;
    private Collisions cols;

    private void Awake()
    {
        sp = GetComponentInChildren<SpriteRenderer>();
        shouldFlip = GetComponentInChildren<AnimSetter>() != null;
        MovingObject.transform.position = start.position;

        cols = GetComponentInChildren<Collisions>();
    }
    private void Start()
    {
        PlayerStateMachine.OnStateChanged += onPlayerStateChange;
        endGame.OnGameEnd += IncreaseSpeed;
    }
    void onPlayerStateChange(Istate newState)
    {
        if (cols == null) return;

        if (cols.dead) return;

        if (newState is Move)
            shouldMove = true;
    }
    private void IncreaseSpeed() // used when the game ends
    {
        speed = 0.5f;
    }

    public void resetPlatform()
    {
        Invoke("resetDelay", 2);
    }
    void resetDelay() //reset the platofrm entirely
    {
        shouldMove = false;
        percComplete = 0;
        direction = 1; 
        MovingObject.transform.position = start.position; // set postiino to start position
    }





    void Update()
    {
        if (!shouldMove) return;

        percComplete += Time.deltaTime * speed * direction; // increase perc complete by speed and curent direction.  

        MovingObject.transform.position = Vector3.Lerp(start.position, end.position, percComplete); // lerp the position of the object from start to end by perc complete

        if (percComplete >= 1 && direction == 1) // spaw direction
        {

            direction = -1;
            if (shouldFlip) // if enemy object flip the sprite
            {
                sp.flipX = !sp.flipX;
            }
        }
        else if (percComplete <= 0 && direction == -1)// spaw direction
        {
            direction = 1;
            if (shouldFlip)// if enemy object flip the sprite
            {
                sp.flipX = !sp.flipX;
            }

        }
    }

    public void SetShouldMove(bool value)
    {
        shouldMove = value;
    }

    private void OnDisable() // de register from events
    {
        PlayerStateMachine.OnStateChanged -= onPlayerStateChange;
        endGame.OnGameEnd -= IncreaseSpeed;
    }
}
