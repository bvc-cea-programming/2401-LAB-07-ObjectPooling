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

        GameObject shootObj = Instantiate(bullet, shootPosition);
        shootObj.transform.parent = null;
        shootObj.GetComponent<Rigidbody>().AddForce(shootPosition.forward * shootSpeed, ForceMode.Acceleration);
        StartCoroutine(Wait(shootObj));

        // Instantiate an "arrow basic" object and shoot towards the forward direction
        // tip: use ForceMode.Acceleration
        // Destroy the object after few seconds
    }

    IEnumerator Wait(GameObject obj)
    {
        yield return new WaitForSeconds(2);
        Destroy(obj);
    }
}
