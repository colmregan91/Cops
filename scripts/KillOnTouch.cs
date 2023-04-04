using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class KillOnTouch : MonoBehaviour
{
    public PooledObject particle;
    public Vector3 particleoffset; // where objects should be placed
    public deathSounds sound;// set in inspector 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.wasHitByPlayer()) // if hits player
        {
            particle.GetFromPool(collision.transform.position + particleoffset);
            if (!GameManager.Instance.getPlayerDead())
            {

                GameManager.Instance.KillPlayer(sound);
            }
        }
    }
}
