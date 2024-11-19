using UnityEngine;

public class WeaponShootPooled : MonoBehaviour
{
    [SerializeField] private float shootSpeed = 700f;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float cooldownWindow = 0.1f;
    [SerializeField] private ObjectPool bulletPool;

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
        
        // Return if the bullet pool is null
        if (bulletPool == null)
        {
            return;
        }

        // get an object from the pool
        // set the position and the rotation
        PooledObject bullet = bulletPool.GetPooledObject();

        bullet.transform.position = shootPosition.position;
        bullet.transform.rotation = shootPosition.rotation;
        // shoot the object
        bullet.GetComponent<Rigidbody>().AddForce(shootPosition.forward * shootSpeed);
        // destroy or release the object after 2 seconds.
        bullet.Release(2f);
    }
}
