using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float LifeTime = 5f;
    GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (LifeTime <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            LifeTime -= Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged" || collision.gameObject.tag == "Ennemy")
        {
            Destroy(gameObject);
        }
    }
}
