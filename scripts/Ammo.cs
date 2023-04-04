using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ammo : MonoBehaviour
{
    public Sprite handGun; // handgun image
    public Sprite mGun;// machine gun image

    public TextMeshProUGUI text;// ammo count text
    public Image img;
    private WeaponFire weaponFire; // weapon fire instance on player
    private int ammo;
    public GameObject ammoParent; // game object that holds the ammo UI
    void Start()
    {
        weaponFire = FindObjectOfType<WeaponFire>();
        weaponFire.OnWeaponPickedUp += HandleWeapon;
        weaponFire.OnWeaponUsed += OnWeaponUsed;
    }

    private void OnDisable()
    {
        weaponFire.OnWeaponPickedUp -= HandleWeapon;
        weaponFire.OnWeaponUsed -= OnWeaponUsed;
    }

    private void HandleWeapon(IShoot obj)
    {
        ammoParent.SetActive(true);

        if (obj is Handgun) img.sprite = handGun;

        if (obj is Machinegun) img.sprite = mGun;

        ammo = obj.startAmmo();

        text.text = ammo.ToString();
    }

    private void OnWeaponUsed(int ammo)
    {
        text.text = ammo.ToString();
        if (ammo <= 0)
        {
            ammoParent.SetActive(false);
        }
    }

}
