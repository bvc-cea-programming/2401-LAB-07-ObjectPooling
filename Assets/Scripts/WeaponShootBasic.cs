using System;
using System.Collections;
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
        // tip: use ForceMode.Acceleration
        // Destroy the object after few seconds
        GameObject tempBullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
        tempBullet.GetComponent<Rigidbody>().AddForce(tempBullet.transform.forward*shootSpeed, ForceMode.Acceleration);
        StartCoroutine(DestroyArrow(tempBullet, 2));
    }

    IEnumerator DestroyArrow(GameObject Arrow, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(Arrow);
    }
}
