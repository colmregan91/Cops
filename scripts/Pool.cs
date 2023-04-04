using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public Queue<PooledObject> pooledObjects = new Queue<PooledObject>(); // queue of objects in this pool

    public static Dictionary<PooledObject, Pool> Pools = new Dictionary<PooledObject, Pool>(); // static container for all pools and their pooled objects
    public static GameObject BasePoolHolder; // base obj for all pools

    public PooledObject PooledObject;// the type of object this pool will hold


    public static Pool GetPool(PooledObject obj) // called from pooled object class so we can access the pool it relates to
    {
        if (BasePoolHolder == null)
        {
            BasePoolHolder = new GameObject("[POOLS]"); // create a base for all pools
            BasePoolHolder.transform.position = Vector3.zero;
        }

        if (Pools.ContainsKey(obj))
        {
            return Pools[obj]; // Pool dictionary already contains a pool for this item so return it.  
        }

        GameObject newPoolGameobject = new GameObject($"{obj.name}"); // new pool holder
        newPoolGameobject.transform.position = Vector3.zero;
        newPoolGameobject.transform.SetParent(BasePoolHolder.transform);// set pool holder as child of BasePoolHolder
        Pool newPool = newPoolGameobject.AddComponent<Pool>(); // add pool component to this object
        newPool.PooledObject = obj; // set the type of object this pool will hold.
        newPool.GrowPool(); // grow the pool
        Pools.Add(obj, newPool); // add new pool to our dictionary of pools
        return newPool;// return the new pool
    }


    public void GrowPool()
    {
        for (int i = 0; i < PooledObject.poolSize; i++) // run a loop from 0 to the requested pool size
        {
            PooledObject newObj = Instantiate(PooledObject, Vector3.zero, Quaternion.identity); // instantiate a new instance of the pooled object and reset its transform
            newObj.ReturnToPool += ReturnToPool; // subscribe this object to the return to pool event
            newObj.transform.SetParent(this.transform); // set the parent of this new obj to this pool holder
            pooledObjects.Enqueue(newObj); // add it to the pool queue
        }
    }

    public PooledObject GetPooledObj(Vector3 pos, bool flipX = false)
    {

        if (pooledObjects.Count == 0)
        {
            GrowPool(); // if there are no objects left in the pool, grow it 
        }

        var obj = pooledObjects.Dequeue(); // remove the next pooled object from the pool.  

        obj.transform.SetParent(null); // remove the parent
        obj.transform.position = pos; // set its position to the position we pass in.  
        obj.SetFlipped(flipX); // set whether the image should be flipped or not
        obj.gameObject.SetActive(true); // turn the gameovject on
    
        return obj; // return the gameobjectc.

    }


    public void ReturnToPool(PooledObject obj)
    {
        pooledObjects.Enqueue(obj); // add the object to the top of the pool
        obj.transform.position = Vector3.zero; // reset transform
        obj.transform.SetParent(this.transform); // reset paraent
        obj.gameObject.SetActive(false); // turn off object

    }

    private void OnDisable()
    {
        foreach(var obj in pooledObjects) // loop through each element of the queue
        {
            obj.ReturnToPool += ReturnToPool; // unsubscribe from the event
        }
    }



}
