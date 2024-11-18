using System.Collections;
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
       
            StartCoroutine(ReleaseAfterTime(time));
        // release if the time is 0. release after "time" otherwise
    }
    IEnumerator ReleaseAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Release();
    }
    public void Release()
    {
        _pool.ReturnToPool(this);
        // return the object back in the object pool
    }
}
