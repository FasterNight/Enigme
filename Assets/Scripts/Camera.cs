using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Camera : MonoBehaviour
{

    public Transform target; 
    public Vector3 offset = new Vector3(0, 1, -10); 
    public float rotationSpeed = 3f;
    public Transform Player;

    private float yaw = 0f;
    private float pitch = 0f;

    // Start is called before the first frame update
    private void Start()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            Vector3 lookAtTarget = target.position + Vector3.up * 1.5f;
            transform.LookAt(lookAtTarget);
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    } 

    // Update is called once per frame
    void Update()
    {

        if (target == null) return;

        
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;  
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;  
        pitch = Mathf.Clamp(pitch, -30f, 60f); 
        
    }
    void LateUpdate()
    {
        if (target == null) return;

       
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);  
        transform.position = target.position + rotation * offset;

        Vector3 RotatePlayer = new Vector3(0, yaw, 0);
        Player.transform.Rotate(RotatePlayer);

        transform.LookAt(target);
    }
}


