using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endGame : MonoBehaviour
{

    public static Action OnGameEnd;
    public ParticleSystem ps;
    public GameObject complete;
    private PlayerStateMachine psm;

    private void Start()
    {
        psm = FindObjectOfType<PlayerStateMachine>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.getPlayerDead()) return; // if player not dead
        if (collision.tag == "Player")
        {
            ps.Play();
            soundManager.Instance.playSFXsound(soundEffects.finishGame);
            OnGameEnd?.Invoke(); // tell system game has ended
            psm.enabled = false; // disable player movement
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.Instance.getPlayerDead()) return;// if player not dead
        if (collision.tag == "Player")
        {
            complete.SetActive(true); 
            collision.gameObject.SetActive(false);  // turn off the player game object
            Invoke("backToMainMenu", 2f); // go back to menu in 2 seconds.
        }
    }

    void backToMainMenu()
    {
        ReturnButton.SetLevelToLoad();
    }
}
