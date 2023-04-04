using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int lives;

    public event Action<int> onLivesChanged;
    public event Action<int> onCoinPickedUp;

    public int coins = 0;
    public bool gameover;
    private bool isPlayerDead;

    public TextMeshProUGUI text;

    private void Awake()
    {
        if (Instance != null) // set up the gamemanager singleton
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (loadOption.loadProg != null) // if the player is loading, set the players values to the loaded session
        {
            var prog = loadOption.loadProg;
            GameStateMachine.SetPlayerName(prog.name);
            FindObjectOfType<PlayerStateMachine>().transform.position = prog.pos;
            FindObjectOfType<backgroundCheck>().SetBackground(prog.backgroundIndex);
            soundManager.Instance.ChangeBackgroundVolume(prog.BackgroundVolume);
            soundManager.Instance.ChangeSFXVolume(prog.SFXvolume);
            soundManager.Instance.ChangeJumpVolume(prog.jumpVolume);
            lives = prog.lives;
            coins = prog.coins;
            onCoinPickedUp?.Invoke(coins);
            onLivesChanged?.Invoke(lives);
        }

        if (GameStateMachine.GetPlayerName().Equals("")) // if no name was entered chang eit to no name
        {
            GameStateMachine.SetPlayerName("No Name");
        }

        if (text != null)
        {
            text.text = GameStateMachine.GetPlayerName(); // display the text at top of screen
        }
    }

    public void AddCoin() // add a coin when the player colldies with one
    {
        coins++;
        onCoinPickedUp?.Invoke(coins);

    }

    public void AddLife()// reset coins and increase life when the player hits 10 coins
    {
        coins = 0;
        onCoinPickedUp?.Invoke(coins);
        soundManager.Instance.playGamesound(gameSounds.checkpoint); // play happy sound
        lives++;
        onLivesChanged?.Invoke(lives);
    }
    public void LoseLife()
    {

        lives--;
        onLivesChanged?.Invoke(lives);

    }


    public void KillPlayer(deathSounds sound)
    {
        isPlayerDead = true;
        soundManager.Instance.PlayDeathClip(sound); // play death sound
    }

    public void ResetStats() // reset lives and in the UI
    {
        lives = 3;
        coins = 0;
        onCoinPickedUp?.Invoke(coins);
        onLivesChanged?.Invoke(lives);
    }

    public void ActivatePlayer() // erspawn the player
    { 
        isPlayerDead = false;
    }


    public bool getPlayerDead()
    {
        return isPlayerDead;
    }

    public int getCoins()
    {
        return coins;
    }

    public int getLives()
    {
        return lives;
    }

    private void OnDisable()
    {
        Pool.Pools.Clear();

        gameover = false;
    }

    public void InvokeRespawnOrGameover() // check lives in 2 seconds
    {
        Invoke("RespawnOrGameover", 2f); // check respawn or game over in 2 seconds
    }

    void RespawnOrGameover()
    {
        if (lives <= 0)
        {
            if (gameover) return;

            gameover = true;

        }
        else
        {
            gameover = false;
            SendPlayerToCheckPoint(); // respawn player back to most previous checkpoint

        }
    }

    public void SendPlayerToCheckPoint()
    {
        var checkpoint = checkpointmanager.Instance.getLastPassedCheckPoint(); // get last ck passed
        var psm = FindObjectOfType<PlayerStateMachine>();
        psm.transform.position = checkpoint.gameObject.transform.position; // set player position to this position
        ActivatePlayer(); // activate the player
    }
}
