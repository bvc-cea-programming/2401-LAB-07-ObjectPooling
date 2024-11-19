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

        // Instantiate an "arrow basic" object and shoot towards the forward direction
        GameObject BulletPrefab = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        Rigidbody bulletRb = BulletPrefab.GetComponent<Rigidbody>();
        // tip: use ForceMode.Acceleration
        if (bulletRb != null)
        {
            bulletRb.AddForce(shootPosition.forward * shootSpeed, ForceMode.Acceleration);
        }
        // Destroy the object after few seconds
        Destroy(BulletPrefab, 2f);
    }
}
