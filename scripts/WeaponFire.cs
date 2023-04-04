using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponFire : MonoBehaviour
{
    public PooledObject bullet; // pooled bullet
    private Animator anim;
    private float shootTime; // delay in shooting
    private SpriteRenderer spr;

    private IShoot weapon; // current eeapon
    private int ammo; 
    public Action<IShoot> OnWeaponPickedUp;
    public Action<int> OnWeaponUsed;
    bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
   
        PlayerStateMachine.OnStateChanged += HandlePlayerStateChange;
    }

    private void OnDisable()
    {
        PlayerStateMachine.OnStateChanged -= HandlePlayerStateChange;
    }

    public void EquipWeapon(IShoot newWeapon)
    {
        soundManager.Instance.playSFXsound(soundEffects.gunClick); // play gun click sound
        anim.SetBool("Shoot", false);
        weapon = newWeapon; // set new weapon to the weapon picked up
        ammo = newWeapon.startAmmo();
        OnWeaponPickedUp?.Invoke(weapon);

    }

    void HandlePlayerStateChange(Istate state)
    {
        if (state is Die || state is GameOver)
        {
         
            anim.SetBool("Shoot", false);
            SetCanShoot(false);
        }
        else
        {
            SetCanShoot(true);
        }
    }

    void SetCanShoot(bool value)
    {
        canShoot = value;
    }


    void Update()
    {
        if (!canShoot) return;

        if (weapon == null)
        {
            anim.SetBool("Shoot", false);
            return;
        }

        if (weapon is Machinegun)
        {
            ShootMachineGun();
            return;
        }

        if (weapon is Handgun)
        {
            ShootHandgun();
        }
    }

    void ShootHandgun()
    {
        if ((shootTime + weapon.FireRate()) <= Time.time)
        {

            if (weapon.GetKey())
            {
                if (anim.GetBool("Shoot") == false)
                {
                    anim.SetLayerWeight(1, 0);// set weight layer
                    anim.SetLayerWeight(2, 1);
                    anim.SetLayerWeight(weapon.animLayer(), 1); 
                    anim.SetBool("Shoot", true);
                   
                }
            }
        }
    }


    void ShootMachineGun()
    {
        if ((shootTime + weapon.FireRate()) <= Time.time) // if player can shoot
        {
            if (weapon.GetKey())
            {
                if (anim.GetBool("Shoot") == false)
                {
                    anim.SetLayerWeight(1, 0);
                    anim.SetLayerWeight(2, 0);
                    anim.SetLayerWeight(weapon.animLayer(), 1);
                    anim.SetBool("Shoot", true);
                }
                ShootMgun();
            }
            else
            {
                anim.SetBool("Shoot", false);
            }
        }
    }
    public void ShootHgun()
    {
        shootTime = Time.time; // for shot delay
        bullet.GetFromPool(transform.position, spr.flipX); // get bullet at this pos and current value of flip x
        bullet.SetFlipped(spr.flipX);
        anim.SetLayerWeight(1, 0); // set weight of layer 1
        anim.SetLayerWeight(2, 0);
        anim.SetBool("Shoot", false);
        soundManager.Instance.playSFXsound(soundEffects.shoot);
        HandleWeaponFired(); // decuct ammo and invoke onWeaponUsed event
    }

    public void ShootMgun()
    {

        shootTime = Time.time;
        bullet.GetFromPool(transform.position, spr.flipX);
        bullet.SetFlipped(spr.flipX);
        soundManager.Instance.playSFXsound(soundEffects.shoot); // play shot sound
        HandleWeaponFired();
    }


    void HandleWeaponFired()
    {
        ammo--;
        OnWeaponUsed?.Invoke(ammo);
        if (ammo <= 0)
        {
            weapon = null;
        }
    }
}


