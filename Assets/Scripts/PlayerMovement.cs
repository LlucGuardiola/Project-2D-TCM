using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    [SerializeField] private int maxJumpCount;      
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer; // Detecció de col·lisió amb ground 
    [SerializeField] TrailRenderer tr;

    private bool canDash = true;
    public bool IsDashing { get; private set; }

    public bool CanGetDamage { get; private set; } = true;
    public bool CanMove;
    private BoxCollider2D boxCollider; 
    private static Rigidbody2D body;
    private Animator animator;
    private int jumpCount;
    private bool canJump;
    private bool isJumping;

    public bool ApplyingInput { get; private set; }              // Decideix si l'sprite ha de fer flip en eix x o y (si està mirant dreta o esq)
    private PhysicsMaterial2D defaultMaterial, noFrictionMaterial; /* Material default i material sense
                                                                       fricció (no es queda enganxat a les parets) */

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = 0;

        boxCollider = GetComponent<BoxCollider2D>();
        defaultMaterial = GetComponent<PhysicsMaterial2D>();
        noFrictionMaterial = new PhysicsMaterial2D();
        noFrictionMaterial.friction = 0;

        CanMove = true;
        canJump = true;
    }
    private void Update()
    {
        Movement();
        
        Jump();

        Teleport();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !GetComponent<PlayerAttack>().isAtacking)
        {
            StartCoroutine(Dash(0.2f, 1, 30));
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && !GetComponent<PlayerAttack>().isAtacking)
        {
            jumpCount++;
            body.velocity = new Vector2(body.velocity.x, jumpHeight * 2);
        }
        canJump = CanJump();
        animator.SetBool("canJump", isJumping);
    }
    private bool CanJump()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        if (hit.collider != null) // Si NO està a l'aire
        {
            body.GetComponent<Rigidbody2D>().sharedMaterial = defaultMaterial;
            jumpCount = 0;
            isJumping = false;

            return true;
        }
        else                      // Si ESTÀ a l'aire
        {
            body.GetComponent<Rigidbody2D>().sharedMaterial = noFrictionMaterial;
            isJumping = true;

            if (jumpCount < maxJumpCount - 1) { return true; }
            else { return false; }
        }
    }
    private void Teleport() // Teleport clicando (1,2,3,4) para canviar de escenas.
    {
        if (Input.GetKey(KeyCode.Alpha1)) { body.transform.position = new Vector3(2f, 2f, 0f); }
        if (Input.GetKey(KeyCode.Alpha2)) { body.transform.position = new Vector3(261f, 2.3f, 0f); }
        if (Input.GetKey(KeyCode.Alpha3)) { body.transform.position = new Vector3(525f, -1f, 0f); }
        if (Input.GetKey(KeyCode.Alpha4)) { body.transform.position = new Vector3(796f, 4f, 0f); }
    }
    
    public void Movement()
    {
        if (!CanMove) return;  
        if (IsDashing) return; 

        float horizontalInput = Input.GetAxis("Horizontal");

        if (!GetComponent<PlayerAttack>().isAtacking)
        {
            body.velocity = new Vector2(horizontalInput * Speed, body.velocity.y);
            if (horizontalInput < 0) { body.GetComponent<SpriteRenderer>().flipX = false; }
            else if (horizontalInput > 0) { body.GetComponent<SpriteRenderer>().flipX = true; }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && !GetComponent<PlayerAttack>().isAtacking) 
        { ApplyingInput = true; }
        else
        {
            ApplyingInput = false;
            if (body.velocity.x != 0 && !isJumping)
            {
                body.velocity = new Vector2(body.velocity.x * 0.1f, body.velocity.y);
            }
        }

        animator.SetBool("run", body.velocity.x != 0 && ApplyingInput);
    }
    private IEnumerator Dash(float dashingTime, float dashingCooldown, float dashingPower)
    {
        canDash = false;
        IsDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        tr.emitting = true;
        body.velocity = GetComponent<SpriteRenderer>().flipX == true ? new Vector2( 1 * dashingPower, 0.1f) : 
                                                                       new Vector2(-1 * dashingPower, 0.1f);

        CanGetDamage = false;

        yield return new WaitForSeconds(dashingTime * 1.1f);
        body.gravityScale = originalGravity;
        IsDashing = false;
        tr.emitting = false;
        CanGetDamage = true;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private void OnTriggerEnter2D(Collider2D collision) // Respawn i checkpoints 
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            GetComponent<ManageRespawn>().NewCheckpoint(collision.gameObject.transform.position);
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
        }
        if (collision.gameObject.CompareTag("Deathzone"))
        {
            GetComponent<ManageRespawn>().HasToRespawn = true;
            GetComponent<HealthManager>().LoseLife(10);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("elevator"))
        {
            transform.parent = collision.gameObject.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("elevator"))
        {
            transform.parent = null;
        }
    }
}

