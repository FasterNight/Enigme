using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Transform playerTransform; 
    public float rotationSpeed = 50f; 
    public float lifeTime = 1f; 
    public float orbitRadius = 1f;
    private Transform Orientation;

    private float currentAngle = 1f;

    private void Start()
    {
        Orientation = FindFirstObjectByType<Camera>().transform;
    }

    void Update()
    {

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }


        if (playerTransform != null)
        {
            currentAngle += rotationSpeed * Time.deltaTime;
            float radians = currentAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(radians), 0, Mathf.Sin(radians)) * orbitRadius;
            transform.position = playerTransform.position + offset;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
