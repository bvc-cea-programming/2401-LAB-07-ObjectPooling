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
        poolStack = new Stack<PooledObject>(poolSize);
        
        for (int i = 0; i < poolSize; i++)
        {
            PooledObject tempObject = Instantiate(objectToPool, transform);
            tempObject.SetPool(this);
            tempObject.gameObject.SetActive(false);
            poolStack.Push(tempObject);
        }
        // Initialize the Stack
        // Create a temp Pooled Object
        // Using a loop, and the temp pooled object or otherwise, populate the stack.
        // while doing so, let the pooled object know about the object pool as well
        // keep the object deactivated at start as well.
        
    }

    public PooledObject GetPooledObject()
    {
        // if the stack is empty, create a new object and return it
        if (poolStack.Count == 0)
        {
            PooledObject newObject = Instantiate(objectToPool, transform);
            newObject.SetPool(this);
            return newObject;
        }
        PooledObject pooledObject = poolStack.Pop();
        pooledObject.gameObject.SetActive(true);
        
        return pooledObject;

        // remove an object from the stack, activate it and return it.
        return null;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        poolStack.Push(pooledObject);
        // add the object back in to the stack, and deactivate it
    }
}
