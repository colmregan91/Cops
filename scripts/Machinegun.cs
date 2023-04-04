using UnityEngine;

public class Machinegun : IShoot
{


    public int animLayer()
    {
        return 1;
    }

    public float FireRate()
    {
        return 0.3f;
    }

    public bool GetKey()
    {
        return Input.GetKey(KeyCode.B); // holding shoot key
    }

    public float GunBlend()
    {
        return 2;
    }

    public int startAmmo()
    {
        return 10;
    }
}


