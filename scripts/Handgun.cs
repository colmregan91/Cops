using UnityEngine;

public class Handgun : IShoot
{
    public int animLayer()
    {
        return 2;
    }

    public float FireRate()
    {
        return 1;
    }

    public bool GetKey()
    {
        return Input.GetKeyDown(KeyCode.B); // shoot button
    }

    public float GunBlend()
    {
        return 0;
    }

    public int startAmmo()
    {
        return 3;
    }
}


