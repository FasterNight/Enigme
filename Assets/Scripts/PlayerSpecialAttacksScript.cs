using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpecialAttacksScript : MonoBehaviour
{
    public float speAtkPts, maxSpeAtkPts = 16, speAtkCharge, speAtkCooldown;
    public int speAtkLvl;
    public bool isDoingSpecialAttack, isDoingSpecialAttackFire1, isDoingSpecialAttackFire2, isDoingSpecialAttackFire3, isDoingSpecialAttackIce1, isDoingSpecialAttackIce2, isDoingSpecialAttackIce3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && speAtkPts >= 3)
        {
            speAtkLvl = 1;
        }
        if (Input.GetKey(KeyCode.C) && speAtkPts >= 3)
        {
            speAtkCharge += Time.deltaTime;
            if (speAtkCharge >= 0.8f && speAtkPts >= (3 * speAtkLvl +3) && speAtkLvl < 3)
            {
                speAtkLvl += 1;
                speAtkCharge = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.C) && speAtkLvl > 0 || speAtkCharge >= 1.2f)
        {
            speAtkPts -= 3 * speAtkLvl;
            isDoingSpecialAttack = true;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Get the PlayerElementScript component from the player GameObject
                PlayerElementScript playerElement = player.GetComponent<PlayerElementScript>();
                if (playerElement != null)
                {
                    if (playerElement.isFireElement)
                    {
                        switch (speAtkLvl)
                        {
                            case 1:
                                isDoingSpecialAttackFire1 = true;
                                speAtkCooldown = 1.0f;
                                break;

                            case 2:
                                isDoingSpecialAttackFire2 = true;
                                speAtkCooldown = 2.0f;
                                break;

                            case 3:
                                isDoingSpecialAttackFire3 = true;
                                break;

                            default:
                                break;
                        }
                    }
                    else if (playerElement.isIceElement)
                    {
                        switch (speAtkLvl)
                        {
                            case 1:
                                isDoingSpecialAttackIce1 = true;
                                break;

                            case 2:
                                isDoingSpecialAttackIce2 = true;
                                break;

                            case 3:
                                isDoingSpecialAttackIce3 = true;
                                break;

                            default:
                                break;
                        }
                    }
                }
            }

            speAtkLvl = 0;
            speAtkCharge = 0;
        }
    }
    private void FixedUpdate()
    {
        if (speAtkPts < maxSpeAtkPts)
        {
            speAtkPts += Time.deltaTime * 0.2f;
        }
        if (speAtkPts > maxSpeAtkPts)
        {
            speAtkPts = maxSpeAtkPts;
        }
        speAtkCooldown -= Time.deltaTime;
        if (speAtkCooldown <= 0)
        {
            isDoingSpecialAttack = false;
            isDoingSpecialAttackFire1 = false;
            isDoingSpecialAttackFire2 = false;
            isDoingSpecialAttackFire3 = false;
            isDoingSpecialAttackIce1 = false;
            isDoingSpecialAttackIce2 = false;
            isDoingSpecialAttackIce3 = false;
        }

        if (isDoingSpecialAttackFire1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player playerScript = player.GetComponent<Player>();
            playerScript.Shoot();
        }
        if (isDoingSpecialAttackFire2)
        {
            GameObject Camera = GameObject.Find("Main Camera");
            Camera cameraScript = Camera.GetComponent<Camera>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player playerScript = player.GetComponent<Player>();
            if (playerScript.isGrounded)
            {
                playerScript.Jump();
                cameraScript.pitch = 65f;
            }
            if (speAtkCooldown <= 1.0f)
            {
                player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.1f, player.transform.position.z);
                cameraScript.pitch -= 1f;
                playerScript.Shoot();
            }
        }
        if (isDoingSpecialAttackFire3)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player playerScript = player.GetComponent<Player>();
            playerScript.Shoot();
        }
        if (isDoingSpecialAttackIce1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player playerScript = player.GetComponent<Player>();
            playerScript.Shoot();
        }
        if (isDoingSpecialAttackIce2)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player playerScript = player.GetComponent<Player>();
            playerScript.Shoot();
        }
        if (isDoingSpecialAttackIce3)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Player playerScript = player.GetComponent<Player>();
            playerScript.Shoot();
        }
    }
}
