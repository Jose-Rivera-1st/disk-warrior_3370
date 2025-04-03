using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    public bool isRedPlayer = true;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Sprite idleSprite;   // standing
    public Sprite jumpSprite;   // in-air

    private SpriteRenderer spriteRenderer;

    public int maxShots = 3;
    private int currentShots = 0;
    public float cooldownDuration = 3f;
    private float cooldownTimer = 0f;
    private bool isCoolingDown = false;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //_____________________________________________
        // Check if touching ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        //_____________________________________________
        // swap sprites, from idle to jumping
        if (isGrounded && spriteRenderer.sprite != idleSprite)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (!isGrounded && spriteRenderer.sprite != jumpSprite)
        {
         spriteRenderer.sprite = jumpSprite;
        }
        //_____________________________________________
        // flip sprite when jumping , and idle
        if (movement.x > 0)
            spriteRenderer.flipX = false;
        else if (movement.x < 0)
            spriteRenderer.flipX = true;



        if (isRedPlayer)
        {
            //_____________________________________________
            // WASD movement
            movement.x = Input.GetKey(KeyCode.D) ? 1 : Input.GetKey(KeyCode.A) ? -1 : 0;

            if (Input.GetKeyDown(KeyCode.W) && isGrounded)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            if (Input.GetKey(KeyCode.S))
                rb.linearVelocity += Vector2.down * 20f * Time.deltaTime;
        }
        else
        {
            //_____________________________________________
            // Arrow key movement
            movement.x = Input.GetKey(KeyCode.RightArrow) ? 1 : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0;

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            if (Input.GetKey(KeyCode.DownArrow))
                rb.linearVelocity += Vector2.down * 20f * Time.deltaTime;
        }
        //_____________________________________________
        // shooting letter for both players

        //_____________________________________________
        // cooldown while shooting 
        if (!isCoolingDown)
        {
            if (isRedPlayer && Input.GetKeyDown(KeyCode.F))
            {
                TryShoot(Vector2.right);
            }
            if (!isRedPlayer && Input.GetKeyDown(KeyCode.L)) // or your blue shoot key
            {
                TryShoot(Vector2.left);
            }
        }

        if (isCoolingDown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isCoolingDown = false;
                currentShots = 0;
            }
        }

    }


    //_____________________________________________
    // fire disk to openent 
    void Fire(Vector2 dir)
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        proj.GetComponent<Projectile>().direction = dir;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movement.x * moveSpeed, rb.linearVelocity.y);
    }

    //_____________________________________________
    // limit shooting to 3  
    void TryShoot(Vector2 dir)
    {
        if (currentShots < maxShots)
        {
            Fire(dir);
            currentShots++;

            if (currentShots >= maxShots)
            {
                isCoolingDown = true;
                cooldownTimer = cooldownDuration;
            }
        }
    }
}
