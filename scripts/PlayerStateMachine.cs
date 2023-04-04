using UnityEngine;
using System;
public class PlayerStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;

    private Rigidbody2D rb;
    private groundDetection groundDet;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
   

    private Move move;
    private Jump jump;
    private Die die;
    private GameOver gameOver;
    public string state;

    public static Action<Istate> OnStateChanged;

    public Istate getCurrentState()
    {
        return _stateMachine._currentState; // return current state
    }
    void Awake()
    {
        groundDet = GetComponent<groundDetection>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        move = new Move(groundDet, transform, anim, spriteRenderer);
        jump = new Jump(rb, anim, move);
        die = new Die(anim);
        gameOver = new GameOver();

        _stateMachine = new StateMachine();

        _stateMachine.OnStateChanged += state => OnStateChanged?.Invoke(state);
    }


    private void Start()
    {

        _stateMachine.AddTransition(move, jump, () => move.GetJumpRequest());
        _stateMachine.AddTransition(jump, move, () => groundDet.GetIsGrounded());

        _stateMachine.AddTransition(move, die, () => GameManager.Instance.getPlayerDead());
        _stateMachine.AddTransition(jump, die, () => GameManager.Instance.getPlayerDead());

        _stateMachine.AddTransition(die, move, () => !GameManager.Instance.getPlayerDead());
        _stateMachine.AddTransition(die, gameOver, () => GameManager.Instance.gameover);

        _stateMachine.SetState(move);

    }

    private void OnDisable()
    {
        _stateMachine.OnStateChanged -= OnStateChanged;
    }

    private void Update()
    {
        if (Pause.Active) return;

        _stateMachine.Tick();
    }

    public void setCanMove(bool val)
    {
        move.canMove = val;
    }
}
