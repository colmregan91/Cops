using UnityEngine;

public static class Collision2Dextensions
{
    public static bool wasHitOnBottom(this Collision2D collision)
    {
        return collision.contacts[0].normal.y > 0.5f;
    }
    public static bool wasHitByPlayer(this Collision2D collision)
    {
        return collision.gameObject.GetComponent<PlayerStateMachine>();
    }
    public static bool wasHitonTop(this Collision2D collision)
    {
        return collision.contacts[0].normal.y < -0.5f;
    }
    public static bool wasHitRightSide(this Collision2D collision)
    {
      return collision.contacts[0].normal.x < -0.5f;
    }
    public static bool wasHitLeftSide(this Collision2D collision)
    {
        return collision.contacts[0].normal.x > 0.5f;
    }
}
