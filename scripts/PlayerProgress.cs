using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerProgress 
{
    public string name;

    public int coins;
    public int lives;
    public Vector3 pos;
    public IShoot weapon;
    public float ammo;
    public int backgroundIndex;
    public float SFXvolume;
    public float BackgroundVolume;
    public float jumpVolume;
}
