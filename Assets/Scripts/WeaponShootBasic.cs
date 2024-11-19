using System;
using UnityEngine;

public class WeaponShootBasic : MonoBehaviour
{
    
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootSpeed = 700f;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float cooldownWindow = 0.1f;
    [SerializeField] private float destroyTimer = 7;
    
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
        GameObject spawnedBullet = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        
        Rigidbody bulletRigidbody = spawnedBullet.GetComponent<Rigidbody>();
        
        if (bulletRigidbody != null)
        {
            bulletRigidbody.AddForce(shootPosition.forward * shootSpeed, ForceMode.Acceleration);
        }
        Destroy(spawnedBullet,destroyTimer);
        // Instantiate an "arrow basic" object and shoot towards the forward direction
        // tip: use ForceMode.Acceleration
        // Destroy the object after few seconds
    }
}
