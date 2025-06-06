using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerElementScript : MonoBehaviour
{
    public bool isFireElement = true, isIceElement;
    public int elementApplicationValue = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFireElement = !isFireElement;
            isIceElement = !isIceElement;
        }
    }
}

/*
 // Find the player GameObject
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Get the PlayerElementScript component from the player GameObject
                PlayerElementScript playerElement = player.GetComponent<PlayerElementScript>();
                if (playerElement != null)
                {

                }
            }
 */