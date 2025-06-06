using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyScript : MonoBehaviour
{
    public int baseHp, hp, damage;
    public bool isInvincible, isDotInvincible, isTakingFireDot, isTakingIceDot, isStrongEnnemy;
    public float baseSpeed, speed, dotFire, dotIce, lastFireDot, lastIceDot, invincibleTimer, dotInvincibleTimer;
    private FloatingBars healthBar, fireBar, iceBar;
    //public ParticleSystem fireParticle, iceParticle;

    void Start()
    {
        hp = baseHp;
        speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //-------------------- Go to player --------------------
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }

        //-------------------- Check if dead --------------------
        if (hp <= 0)
        {
            EnnemySpawn ennemySpawn = player.GetComponent<EnnemySpawn>();
            if (!isStrongEnnemy)
            {
                ennemySpawn.RemoveEnnemy();
            }
            else
            {
                ennemySpawn.RemoveStrongEnnemy();
            }
            Destroy(gameObject);
        }

        //------------------- Deplete dot if not applied for a while ------------------
        if (lastFireDot <= 0 && dotFire > 0 && isTakingFireDot == false)
        {
            dotFire -= 0.2f;
        }
        if (lastIceDot <= 0 && dotIce > 0 && isTakingIceDot == false)
        {
            dotIce -= 0.2f;
        }
        
        //-------------------- Timer for depleting dots --------------------
        if (lastFireDot > 0)
        {
            lastFireDot -= 0.02f;
        }
        if (lastIceDot > 0)
        {
            lastIceDot -= 0.02f;
        }

        //-------------------- Invincibility for dots --------------------
        if (isDotInvincible)
        {
            dotInvincibleTimer -= Time.deltaTime;
            if (dotInvincibleTimer <= 0)
            {
                isDotInvincible = false;
            }
        }

        //-------------------- Activate dots when 100% and deactivate when 0% --------------------
        if (dotFire >= 100)
        {
            isTakingFireDot = true;
            /*fireParticle = GetComponent<ParticleSystem>();
            fireParticle.Play();
            Destroy(gameObject, fireParticle.main.duration);*/
        }
        if (dotFire <= 0)
        {
            isTakingFireDot = false;
            dotFire = 0;
        }
        if (isTakingFireDot)
        {
            if (dotFire > 0)
            {
                TakeFireDot();
                dotFire -= 1;
            }
        }

        if (dotIce >= 100)
        {
            isTakingIceDot = true;
        }
        if (dotIce <= 0)
        {
            isTakingIceDot = false;
            speed = baseSpeed;
            dotIce = 0;
        }
        if (isTakingIceDot)
        {
            if (dotIce > 0)
            {
                TakeIceDot();
                dotIce -= 0.2f;
            }
        }

        //-------------------- Invincibility for damage --------------------
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }

        //-------------------- Update health bar --------------------
        healthBar = GetComponentInChildren<FloatingBars>();
        healthBar.UpdateHPBar(hp, baseHp);
        fireBar = GetComponentInChildren<FloatingBars>();
        fireBar.UpdateFireBar(dotFire, 100);
        iceBar = GetComponentInChildren<FloatingBars>();
        iceBar.UpdateIceBar(dotIce, 100);

    }
    public void TakeDamage()
    {
        if (!isInvincible)
        {
            // Find the player GameObject
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Get the PlayerElementScript component from the player GameObject
                PlayerElementScript playerElement = player.GetComponent<PlayerElementScript>();
                if (playerElement != null)
                {
                    // Apply damage logic
                    isInvincible = true;
                    invincibleTimer = 0.3f; // Set invincibility duration
                    if (playerElement.isFireElement)
                    {
                        dotFire += playerElement.elementApplicationValue;
                        lastFireDot = 2.0f; // Reset the timer for fire dot
                    }
                    if (playerElement.isIceElement)
                    {
                        dotIce += playerElement.elementApplicationValue;
                        lastIceDot = 2.0f; // Reset the timer for ice dot
                    }
                }
            }
        }
    }
    public void TakeFireDot()
    {
        if (!isDotInvincible)
        {
            hp -= 5;
            isDotInvincible = true;
            dotInvincibleTimer = 0.3f; // Set invincibility duration for dots
        }
    }
    public void TakeIceDot()
    {
        if (!isDotInvincible)
        {
            speed = baseSpeed * 0.5f;
            isDotInvincible = true;
            dotInvincibleTimer = 2.0f; // Set invincibility duration for dots
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Attack" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Player")
        {
            TakeDamage();
        }

    }
}