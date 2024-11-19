using System.Collections;
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
        if (!bulletPool) return;
        // get an object from the pool
        var pooledObj = bulletPool.GetPooledObject();
        // set the position and the rotation
        pooledObj.gameObject.transform.position = shootPosition.position;
        pooledObj.gameObject.transform.rotation = shootPosition.rotation;
        pooledObj.transform.parent = null;
        // shoot the object
        pooledObj.gameObject.GetComponent<Rigidbody>().AddForce(shootPosition.forward * shootSpeed, ForceMode.Acceleration);
        // destroy or release the object after 2 seconds.
        pooledObj.Release(2);
    }
}
