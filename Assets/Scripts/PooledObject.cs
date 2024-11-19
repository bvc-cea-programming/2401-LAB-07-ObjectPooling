using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool _pool;

    public void SetPool(ObjectPool pool)
    {
        // set the object pool
        _pool = pool;
    }

    public void Release(float time)
    {
        // release if the time is 0. release after "time" otherwise
        Invoke(nameof(Release), time);
    }

    public void Release()
    {
        // return the object back in the object pool
        _pool.ReturnToPool(this);
    }
}
