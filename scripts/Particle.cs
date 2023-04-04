using System.Collections;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))] // attribute that states this component must have a particle system attached.
public class Particle : PooledObject
{
    void OnEnable()
    {
        StartCoroutine("CheckIfAlive"); // start the particle coroutine
    }

    IEnumerator CheckIfAlive()
    {
        ParticleSystem ps = this.GetComponent<ParticleSystem>(); // get the particle system on this object.

        while (true && ps != null)
        {
            yield return new WaitForSeconds(0.5f); // wait every haf second and check if the particle is still active
            if (!ps.IsAlive(true))
            {
                SendObjBacktoPool(); // if the particle is not active, send it back to the pool

            }
        }
    }

}
