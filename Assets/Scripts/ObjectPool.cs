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
        poolStack = new Stack<PooledObject>(poolSize);

        // Create a temp Pooled Object

        // Using a loop, and the temp pooled object or otherwise, populate the stack.
        // while doing so, let the pooled object know about the object pool as well
        // keep the object deactivated at start as well.
        
        for(int i = 0; i < poolSize; i++)
        {
            PooledObject obj = Instantiate(objectToPool);
            obj.SetPool(this);
            obj.gameObject.SetActive(false);
            poolStack.Push(obj);
        }
    }

    public PooledObject GetPooledObject()
    {
        // if the stack is empty, create a new object and return it
        if (poolStack.Count == 0)
        {
            PooledObject newObj = Instantiate(objectToPool);
            newObj.SetPool(this);
            return newObj;
        }

        // remove an object from the stack, activate it and return it.
        PooledObject obj = poolStack.Pop();
        obj.gameObject.SetActive(true); 
        return obj;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        // add the object back in to the stack, and deactivate it
        pooledObject.gameObject.SetActive(false);
        poolStack.Push(pooledObject);
    }
}
