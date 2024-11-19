using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        var tempPoolObj = Instantiate(objectToPool, this.transform);
        tempPoolObj.SetPool(this);
        // Using a loop, and the temp pooled object or otherwise, populate the stack.
        // while doing so, let the pooled object know about the object pool as well
        // keep the object deactivated at start as well.
        for (int i = 0; i < poolSize; i++)
        {
            poolStack.Push(tempPoolObj);
            tempPoolObj.gameObject.SetActive(false);
        }
    }
    
    public PooledObject GetPooledObject()
    {
        // if the stack is empty, create a new object and return it
        if (poolStack.Count == 0)
        {
            var pooledObj = Instantiate(objectToPool, this.transform);
            pooledObj.SetPool(this);
            return pooledObj;
        }
        var poolStackObj = poolStack.Pop();
        poolStackObj.gameObject.SetActive(true);
        return poolStackObj;

        // remove an object from the stack, activate it and return it.
    }
    
    public void ReturnToPool(PooledObject pooledObject)
    {
        // add the object back in to the stack, and deactivate it
        poolStack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }
}
