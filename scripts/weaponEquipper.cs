using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponEquipper : MonoBehaviour
{
    public WeaponType weaponType; // type of weapon defined in inspector
    private SpriteRenderer Spr;

    public Sprite handgunSprite; // weapon sprites
    public Sprite MachineSprite;// weapon sprites

    private IShoot weapon; // weapon to be equipped

    private Handgun hgun;
    private Machinegun mgun;

    public enum WeaponType
    {
        handgun,
        machineGun
    }
 

    private void Awake()
    {
        hgun = new Handgun(); // new implementation of the gun components
        mgun = new Machinegun();// new implementation of the gun components
        Spr = GetComponent<SpriteRenderer>();

    }
    private void Start()
    {
        if (weaponType == WeaponType.handgun) // check type referenced in editor
        {
            Spr.sprite = handgunSprite; // if its a handgun set the image of the object to handgun image
            weapon = hgun; // set the weapon
        }
        else
        {
            Spr.sprite = MachineSprite; // if its a machine gun set the image of the object to machine gun image
            weapon = mgun; // set the weapon
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var weaponFire = collision.gameObject.GetComponent<WeaponFire>(); // if its the player


        if (weaponFire)
        {
            weaponFire.EquipWeapon(weapon); //equip the weapon and turn the gameobject off
            gameObject.SetActive(false);
        }
    }
}
