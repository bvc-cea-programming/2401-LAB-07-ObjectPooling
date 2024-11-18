using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private Transform turretObject;
    [SerializeField] private float rotationSpeed;
    
   
    void Update()
    {
        RotateTurret(Input.GetAxis("Horizontal"));
    }
    
    private void RotateTurret(float rotation)
    {
        turretObject.Rotate(Vector3.up * (rotation * rotationSpeed * Time.deltaTime));
    }    
}
