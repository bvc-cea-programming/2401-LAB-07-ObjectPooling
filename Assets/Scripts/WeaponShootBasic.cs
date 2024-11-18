using System;
using UnityEngine;

public class WeaponShootBasic : MonoBehaviour
{
    
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootSpeed = 700f;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float cooldownWindow = 0.1f;

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
        Debug.Log("SHOOT!");

        // Instantiate the bullet at the shoot position with the same rotation
        GameObject spawnedBullet = Instantiate(bullet, shootPosition.position, shootPosition.rotation);

        // Add force to the bullet to shoot it forward
        Rigidbody bulletRigidbody = spawnedBullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.AddForce(shootPosition.forward * shootSpeed, ForceMode.Acceleration);
        }

        // Destroy the bullet after 2 seconds
        Destroy(spawnedBullet, 2f);
    }
}
