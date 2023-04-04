using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private Animator anim;
    private Mover mover;
    private SpriteRenderer spr;
    public bool dead;
    public PooledObject blood;
    public deathSounds sound;
    bool playerHitFromBehind; // detect where player hits the enemy
    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        mover = GetComponentInParent<Mover>();
    }
    public void KillPlayer(Vector3 bloodPos)
    {
        anim.SetTrigger("KnifeAttack");
        blood.GetFromPool(bloodPos);
        if (playerHitFromBehind) // if player hit from behind
        {
            Invoke("flip", 1f); // turn enemy around in 1 second
        }
      GameManager.Instance.KillPlayer(sound);
    }
    void flip()
    {
        spr.flipX = !spr.flipX;
        playerHitFromBehind = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
         
            Die(true); // 
            return;
        }


        if (collision.wasHitByPlayer())
        {
            if (collision.wasHitonTop()) // if the player bounces on this enemy
            {
                collision.collider.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 800); // add upward force to players rigidbody 
                Die(false);
                return;
            }

            if (collision.wasHitRightSide() && spr.flipX)
            {
                playerHitFromBehind = true;
                spr.flipX = false;

            }
            else if (collision.wasHitLeftSide() && !spr.flipX)
            {
                playerHitFromBehind = true;
                spr.flipX = true;
    
            }
            if (!dead)
            {
                soundManager.Instance.playSFXsound(soundEffects.knife);
                KillPlayer(collision.gameObject.transform.position);
                mover.SetShouldMove(false);
            }
        }

    }

    private void Die(bool needblood)
    {
        if (dead) // if enemy is already dead just play a particle
        {
            blood.GetFromPool(transform.position);
            return;
        }
        dead = true;
        SetCanbeHit(); // change to dead layer
        if (needblood)
        {
            blood.GetFromPool(transform.position);
        }

        soundManager.Instance.PlayDeathClip(deathSounds.die); // play death sound
        mover.SetShouldMove(false);  // stop moving
        anim.SetTrigger("Die");
 
        Invoke("turnOff", 2f);
    }

    public void SetCanbeHit()
    {
        gameObject.layer = LayerMask.NameToLayer("Dead");
    }

    void turnOff()
    {
        gameObject.SetActive(false);
    }
}
