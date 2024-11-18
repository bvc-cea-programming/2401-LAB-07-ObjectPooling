using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int poolSize = 20;
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
        PooledObject newPoo = Instantiate(objectToPool);
        newPoo.gameObject.SetActive(false);
        for (int i = 0; i < poolSize; i++)
        {
           
            poolStack.Push(Instantiate(newPoo));
            poolStack.Peek().SetPool(this);
            poolStack.Peek().gameObject.SetActive(false);
        }
        // Using a loop, and the temp pooled object or otherwise, populate the stack.
        // while doing so, let the pooled object know about the object pool as well
        // keep the object deactivated at start as well.
        
    }

    public PooledObject GetPooledObject()
    {
        // if the stack is empty, create a new object and return it
        if(poolStack.Count == 0)
        {
            PooledObject newObj = Instantiate(objectToPool);
            newObj.SetPool(this);
            poolStack.Push(newObj);
            return newObj;
        }
        poolStack.Peek().gameObject.SetActive(true);
        return poolStack.Pop();
        // remove an object from the stack, activate it and return it.
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        poolStack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
        // add the object back in to the stack, and deactivate it
    }
}
