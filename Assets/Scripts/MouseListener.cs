using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseListener : MonoBehaviour
{
    public float mouseSensitivity = 1500f;
    public Transform playerBody;
    float xRotation = 0f;

    public GameObject bullet;
    
    public Rigidbody projectile;
    public float speed = 20;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Camera tracking
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up, mouseX);
        
        
        //Bullet shooting
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody instantiatedProjectile = Instantiate(projectile,transform.position,transform.rotation)as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0,speed));
        }
    }
}
