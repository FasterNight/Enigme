using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAttacksScript : MonoBehaviour
{
    public float speAtkPts, maxSpeAtkPts = 16, speAtkCharge;
    public int speAtkLvl;
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
            if (speAtkCharge >= 0.8f && speAtkPts >= (3 * speAtkLvl +3))
            {
                speAtkLvl += 1;
                speAtkCharge = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.C) && speAtkLvl > 0 || speAtkCharge >= 1.2f)
        {
            speAtkPts -= 3 * speAtkLvl;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Get the PlayerElementScript component from the player GameObject
                PlayerElementScript playerElement = player.GetComponent<PlayerElementScript>();
                if (playerElement != null)
                {
                    Player playerScript = player.GetComponent<Player>();
                    if (playerElement.isFireElement)
                    {
                        switch (speAtkLvl)
                        {
                            case 1:
                                break;

                            case 2:
                                // fireSpeAtk2
                                break;

                            case 3:
                                // fireSpeAtk3
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
                                // iceSpeAtk1
                                break;

                            case 2:
                                // iceSpeAtk2
                                break;

                            case 3:
                                // iceSpeAtk3
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
    }
}
