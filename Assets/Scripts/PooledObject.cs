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
        if(time == 0)
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
        // return the object back in the object pool
        _pool.ReturnToPool(this);
    }
}
