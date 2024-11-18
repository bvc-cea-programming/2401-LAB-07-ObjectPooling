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

        // Populate the stack with pooled objects
        for (int i = 0; i < poolSize; i++)
        {
            // Create a new instance of the object
            PooledObject tempObject = Instantiate(objectToPool);

            // Assign the pool reference to the object
            tempObject.SetPool(this);

            // Deactivate the object
            tempObject.gameObject.SetActive(false);

            // Push it into the stack
            poolStack.Push(tempObject);
        }
        
    }

    public PooledObject GetPooledObject()
    {
        if (poolStack.Count == 0)
        {
            // If the pool is empty, create a new object
            PooledObject newObject = Instantiate(objectToPool);
            newObject.SetPool(this);
            return newObject;
        }

        // Get an object from the pool
        PooledObject pooledObject = poolStack.Pop();
        pooledObject.gameObject.SetActive(true); // Activate it
        return pooledObject;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        // Deactivate the object and add it back to the pool
        pooledObject.gameObject.SetActive(false);
        poolStack.Push(pooledObject);
    }
}
