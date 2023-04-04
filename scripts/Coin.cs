using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player")) // if hit by player
        {
            soundManager.Instance.playSFXsound(soundEffects.coin);
            GameManager.Instance.AddCoin(); // add coin
            Destroy(gameObject); // destroy the gameobject
        }

    }
}
