using UnityEngine;
using System;
public class PooledObject : MonoBehaviour
{
    public int poolSize;
    public Action<PooledObject> ReturnToPool;

    public virtual void SetFlipped(bool _flipped) // virtual method to be overridden by inheriting classes
    {
     
    }
    public virtual void PerformBehavior() // virtual method to be overridden by inheriting classes
    {
        
    }
    public void SendObjBacktoPool()
    {
        ReturnToPool?.Invoke(this); // send the object back to the pool
    }


    public void GetFromPool(Vector3 pos, bool flipped = false)
    {
        var pool = Pool.GetPool(this); // get the pool that this object belong sto
        var pooledObj = pool.GetPooledObj(pos, flipped); // get the next available object from the pool.  

    }
}
