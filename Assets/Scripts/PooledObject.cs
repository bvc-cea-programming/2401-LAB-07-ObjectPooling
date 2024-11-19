using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool _pool;

    public void SetPool(ObjectPool pool)
    {
        _pool = pool;
        // set the object pool
    }

    public void Release(float time)
    {
        if (time <= 0f)
        {
            Release();
        }
        else
        {
            Invoke(nameof(Release), time);
        }
        // release if the time is 0. release after "time" otherwise
    }

    public void Release()
    {
        if (_pool != null)
        {
            _pool.ReturnToPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
        // return the object back in the object pool
    }
}
