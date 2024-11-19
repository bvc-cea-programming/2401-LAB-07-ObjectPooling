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
        for (int i = 0;i < poolSize; i++)
        {
            // while doing so, let the pooled object know about the object pool as well
            var obj = Instantiate(objectToPool);
            obj.SetPool(this);
            poolStack.Push(obj);
            // keep the object deactivated at start as well.
            obj.gameObject.SetActive(false);
        }
    }

    public PooledObject GetPooledObject()
    {
        // if the stack is empty, create a new object and return it

        PooledObject obj;
        if (poolStack.Count <= 0)
        {
            obj = Instantiate(objectToPool);
            obj.SetPool(this);
            poolStack.Push(obj);
        }
        else  // remove an object from the stack, activate it and return it.
        {
            obj = poolStack.Pop();
            obj.gameObject.SetActive(true);
        }
        return obj;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        // add the object back in to the stack, and deactivate it
        poolStack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
