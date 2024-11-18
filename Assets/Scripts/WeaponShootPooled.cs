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
            Debug.LogError("Bullet pool is not assigned!");
            return;
        }
        
        // get an object from the pool
        PooledObject bullet = bulletPool.GetPooledObject();
        if (bullet == null) return;

        // set the position and the rotation
        bullet.transform.position = shootPosition.position;
        bullet.transform.rotation = shootPosition.rotation;
        
        // Apply force to the bullet and shoot it
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.linearVelocity = Vector3.zero; // Reset velocity
            bulletRigidbody.AddForce(shootPosition.forward * shootSpeed, ForceMode.Acceleration);
        }

        // Release the object back to the pool after 2 seconds
        bullet.Release(2f);

    }
}
