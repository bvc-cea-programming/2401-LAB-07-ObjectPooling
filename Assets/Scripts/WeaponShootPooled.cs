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
        if (bulletPool == null)
        { return; }
        // get an object from the pool
        // set the position and the rotation
        PooledObject tempBullet = bulletPool.GetPooledObject();
        tempBullet.transform.position = this.transform.position;
        tempBullet.transform.rotation = this.transform.rotation;


        // shoot the object
        tempBullet.GetComponent<Rigidbody>().AddForce(tempBullet.transform.forward * shootSpeed, ForceMode.Acceleration);

        // destroy or release the object after 2 seconds.
        StartCoroutine(DeactivateArrow(tempBullet, 2));
    }
    IEnumerator DeactivateArrow(PooledObject Arrow, float t)
    {
        yield return new WaitForSeconds(t);
        bulletPool.ReturnToPool(Arrow);
    }
}
