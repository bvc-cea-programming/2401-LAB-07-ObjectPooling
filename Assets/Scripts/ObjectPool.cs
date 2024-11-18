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

        // Using a loop, and the temp pooled object or otherwise, populate the stack.
        // while doing so, let the pooled object know about the object pool as well
        for (int i = 0; i < poolSize; i++)
        {
            PooledObject pooledObjectUnit = Instantiate(objectToPool);
            pooledObjectUnit.SetPool(this);
            pooledObjectUnit.gameObject.SetActive(false);
            poolStack.Push(pooledObjectUnit);
        }
        // keep the object deactivated at start as well.
        
    }

    public PooledObject GetPooledObject()
    {
        // if the stack is empty, create a new object and return it
        if (poolStack.Count == 0)
        {
            SetupPool();
        }
        PooledObject pooledObjectUnit = poolStack.Pop();
        pooledObjectUnit.gameObject.SetActive(true);
        return pooledObjectUnit;

        // remove an object from the stack, activate it and return it.
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        // add the object back in to the stack, and deactivate it
        pooledObject.gameObject.SetActive(false);
        poolStack.Push(pooledObject);
    }
}
