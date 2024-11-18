using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool _pool;

    // Associate this object with its pool
    public void SetPool(ObjectPool pool)
    {
        // set the object pool
        _pool = pool;
    }

    // Release the object back to the pool after a delay
    public void Release(float time)
    {
        // release if the time is 0. release after "time" otherwise
        if (time <= 0f)
        {
            Release();
        }
        else
        {
            Invoke(nameof(Release), time);
        }
    }

    // Immediately return the object back to the pool
    public void Release()
    {
        // return the object back in the object pool
        if (_pool != null)
        {
            _pool.ReturnToPool(this);
        }
        else
        {
            Debug.LogWarning("No pool assigned to this object. Cannot return to pool");
        }
    }
}
