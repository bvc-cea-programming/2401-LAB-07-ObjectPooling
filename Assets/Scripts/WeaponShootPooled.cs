using UnityEngine;

public class WeaponShootPooled : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 700f;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float cooldownWindow = 0.1f;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private float releaseTime = 1f;
    private float _nextTimeToShoot;
    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && Time.time > _nextTimeToShoot)
        {
            Shoot();
            // set cooldown delay
            _nextTimeToShoot = Time.time + cooldownWindow;
        }
    }

    private void Shoot()
    {
        Debug.Log("SHOOT POOL!");
        PooledObject bullet = bulletPool.GetPooledObject();
        // Return if the bullet pool is null
        bullet.transform.position = shootPosition.position;
        bullet.transform.rotation = shootPosition.rotation;
        
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity = Vector3.zero; // Reset velocity
            bulletRigidbody.AddForce(shootPosition.forward * shootSpeed, ForceMode.Acceleration);
            
        }
        PooledObject _pooledObject = bullet.GetComponent<PooledObject>();
        _pooledObject.Release(releaseTime);
        // get an object from the pool
        // set the position and the rotation
        // shoot the object
        // destroy or release the object after 2 seconds.
    }
}
