using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 10;
    [SerializeField] private PooledObject objectToPool;

    private Stack<PooledObject> poolStack;
    
    private void Start()
    {
        SetupPool();
    }

    private void SetupPool()
    {
        if(!objectToPool) return;

        // Initialize the Stack
        poolStack = new Stack<PooledObject>();
        // Create a temp Pooled Object
        objectToPool.SetPool(this);
        // Using a loop, and the temp pooled object or otherwise, populate the stack.
        foreach (var poolObject in poolStack)
        {
            poolStack.Push(poolObject);
        }
        // while doing so, let the pooled object know about the object pool as well

        // keep the object deactivated at start as well.

    }

    public PooledObject GetPooledObject()
    {
        // if the stack is empty, create a new object and return it
     
        if (poolStack.Count == 0)
        {
            objectToPool = Instantiate(objectToPool);
            return objectToPool;
        }
        else  // remove an object from the stack, activate it and return it.
        {
            poolStack.Pop();
            var pool = poolStack.Pop();
            pool.gameObject.SetActive(true);
            return poolStack.Pop();
        }
  
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        // add the object back in to the stack, and deactivate it
        poolStack.Push((PooledObject)pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
