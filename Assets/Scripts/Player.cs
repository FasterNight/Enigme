using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField]
    private float moveSpeed = 7f;
    [SerializeField]
    private float groundDrag = 5f;
    [SerializeField]
    private float jumpForce = 12f;
    [SerializeField]
    private float jumpCooldown = 0.25f;
    [SerializeField]
    private float airMultiplier = 0.1f;
    [SerializeField]
    private Transform playerCamera;

    [Header("Keybinds")]
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;

    [Header("Other Settings")]
    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField]
    private Transform Orientation;
    private GameObject Camera;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rigidbody;

    private float playerHeight = 1.55f;
    public bool isGrounded;

    private bool readyToJump = true;

    public GameObject bulletPrefab;
    public GameObject attackPrefab;
    public float bulletForce = 50f;
    public Transform shootPoint;
    public Transform attackPoint;
    float Cooldown = 0.2f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;

        Camera = GameObject.Find("Main Camera");
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(jumpKey) && readyToJump && isGrounded)
        {
            Debug.Log("test");

            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void movePlayer()
    {
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        moveDirection = new Vector3(moveDirection.x, 0, moveDirection.z);

        if (isGrounded)
        {
            rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!isGrounded)
        {
            rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

    }

    public void Jump()
    {
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);

        rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, WhatIsGround);

        MyInput();

        if (isGrounded)
        {
            rigidbody.drag = groundDrag;
        }
        else
        {
            rigidbody.drag = 0f;
        }


        PlayerSpecialAttacksScript playerSpecialAttacks = this.GetComponent<PlayerSpecialAttacksScript>();
        if (!playerSpecialAttacks.isDoingSpecialAttack)
        {
            // ShootGun
            if (Input.GetMouseButton(0) && Cooldown <= 0)
            {
                Shoot();
                Cooldown = 0.2f;
            }
            if (Cooldown > 0)
            {
                Cooldown -= Time.deltaTime;
            }

            // Sword
            if (Input.GetMouseButtonDown(1))
            {
                Attack();
            }
        }
    }

    void Attack()
    {


        Vector3 attackDirection = playerCamera.transform.forward;

        Quaternion attackRotation = Quaternion.LookRotation(attackDirection, Vector3.up);
       

        GameObject swordHitbox = Instantiate(attackPrefab, attackPoint.position, attackRotation);

        swordHitbox.transform.SetParent(gameObject.transform);

        Sword swordScript = swordHitbox.GetComponent<Sword>();
        if (swordScript != null)
        {
            swordScript.playerTransform = this.transform;
        }

        Collider playerCollider = GetComponent<Collider>();
        Collider hitboxCollider = swordHitbox.GetComponent<Collider>();
        if (playerCollider != null && hitboxCollider != null)
        {
            Physics.IgnoreCollision(hitboxCollider, playerCollider);
        }

        swordHitbox.GetComponent<MeshRenderer>().enabled = true;
        swordHitbox.GetComponent<BoxCollider>().enabled = true;
        

    }

    public void Shoot()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerSpecialAttacksScript playerSpecialAttacks = player.GetComponent<PlayerSpecialAttacksScript>();
        if (!playerSpecialAttacks.isDoingSpecialAttack || playerSpecialAttacks.isDoingSpecialAttackFire1)
        {
            bulletPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }
        if (playerSpecialAttacks.isDoingSpecialAttackFire1)
        {
            bulletPrefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        }
        if (playerSpecialAttacks.isDoingSpecialAttackFire2)
        {
            bulletPrefab.transform.localScale = new Vector3(2f, 2f, 2f);
        }

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        Collider playerCollider = GetComponent<Collider>();
        Collider bulletCollider = bullet.GetComponent<Collider>();
        if (playerCollider != null && bulletCollider != null)
        {
            Physics.IgnoreCollision(bulletCollider, playerCollider);
        }

        bullet.GetComponent<MeshRenderer>().enabled = true;
        bullet.GetComponent<SphereCollider>().enabled = true;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
            rb.velocity = Orientation.forward * bulletForce;
        }
    }



    private void FixedUpdate()
    {
        movePlayer();
    }

}
