using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyScript : MonoBehaviour
{
    public int baseHp, hp, damage;
    public bool isInvincible, isDotInvincible, isTakingFireDot, isTakingIceDot;
    public float baseSpeed, speed, dotFire, dotIce, lastFireDot, lastIceDot, invincibleTimer, dotInvincibleTimer;


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
            Destroy(gameObject);
        }

        //------------------- Deplete dot if not applied for a while ------------------
        if (lastFireDot <= 0 && dotFire > 0 && isTakingFireDot == false)
        {
            dotFire -= 1;
        }
        if (lastIceDot <= 0 && dotIce > 0 && isTakingIceDot == false)
        {
            dotIce -= 1;
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

    }
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            //hp -= player.Damage;
            isInvincible = true;
            invincibleTimer = 0.3f; // Set invincibility duration
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
}