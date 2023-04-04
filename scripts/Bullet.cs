using UnityEngine;

public class Bullet : PooledObject
{

    public float speed;
    public PooledObject shotParticle;
    private bool flipped;
    public override void SetFlipped(bool _flipped)
    {
        flipped = _flipped;
    }

    public void OnEnable()
    {
        shotParticle.GetFromPool(transform.position + (flipped ? Vector3.left :Vector3.right)); // set whether it should be flipped
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SendObjBacktoPool(); // go back to pool on collision with anything
    }

    private void Update()
    {
        PerformBehavior(); // perform the overridden custom behavior of this pooled obj instance
    }

    public override void PerformBehavior()
    {
        transform.position += speed * (flipped ? Vector3.left : Vector3.right); // move the bullet
    }
}
