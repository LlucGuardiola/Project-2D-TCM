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
    [SerializeField] float vidas;
    [SerializeField] Slider sliderVidas;
    [SerializeField] TrailRenderer tr;

    private bool canDash = true;
    private bool isDashing;

    public bool CanGetDamaged { get; private set; } = true;
    public float fuerzaGolpe;
    private bool puedeMoverse = true;
    private bool hasFallen;
    private BoxCollider2D boxCollider; 
    private static Rigidbody2D body;
    private Animator animator;
    private int jumpCount;
    private bool canJump;
    private bool isJumping;
    public bool MovingRL { get; private set; }              // Decideix si l'sprite ha de fer flip en eix x o y (si està mirant dreta o esq)
    private Vector3 checkpoint;
    private PhysicsMaterial2D defaultMaterial, noFrictionMaterial; /* Material default i material sense
                                                                       fricció (no es queda enganxat a les parets) */

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = 0;
        checkpoint = body.transform.position;

        boxCollider = GetComponent<BoxCollider2D>();
        defaultMaterial = GetComponent<PhysicsMaterial2D>();
        noFrictionMaterial = new PhysicsMaterial2D();
        noFrictionMaterial.friction = 0;

        hasFallen = false;
        canJump = true;

        sliderVidas.maxValue = vidas;
        sliderVidas.value = sliderVidas.maxValue;
    }
    private void Update()
    {
        Movement();
        
        Jump();

        Respawn();

        Teleport();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && GetComponent<PlayerAttack>().isAtacking == false)
        {
            StartCoroutine(Dash(0.2f, 1, 30));
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && GetComponent<PlayerAttack>().isAtacking == false)
        {
            jumpCount++;
            body.velocity = new Vector2(body.velocity.x, jumpHeight * 2);
        }
        canJump = CanJump();
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
    private void ManageRespawn(Vector2 newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }
    private void OnTriggerEnter2D(Collider2D collision) // Respawn i checkpoints ///////////////// codi repetit?
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            ManageRespawn(collision.gameObject.transform.position);
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());
        }
        if (collision.gameObject.CompareTag("Deathzone"))
        {
            hasFallen = true;
            LoseLife(10);
        }
    }
    public void Movement()
    {
        if (!puedeMoverse) return;  
        if (isDashing) return; 

        float horizontalInput = Input.GetAxis("Horizontal");

        if (!GetComponent<PlayerAttack>().isAtacking)
        {
            body.velocity = new Vector2(horizontalInput * Speed, body.velocity.y);
        }

        if (horizontalInput < 0) { body.GetComponent<SpriteRenderer>().flipX = false; }
        else if (horizontalInput > 0) { body.GetComponent<SpriteRenderer>().flipX = true; }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) && !GetComponent<PlayerAttack>().isAtacking) 
        { MovingRL = true; }
        else
        {
            MovingRL = false;
            if (body.velocity.x != 0 && !isJumping)
            {
                body.velocity = new Vector2(body.velocity.x * 0.1f, body.velocity.y);
            }
        }

        animator.SetBool("run", horizontalInput != 0 && MovingRL);
        animator.SetBool("canJump", isJumping);
    }
    public void Respawn()
    {
        if (hasFallen)
        {
            body.transform.position = checkpoint;
            hasFallen = false;
        }
    }
    public void LoseLife(float damageDealt)
    {
        vidas-=damageDealt;
        sliderVidas.value = vidas;

        if (vidas <= 0)
        {
            hasFallen = true; 
            vidas = sliderVidas.maxValue; 
            sliderVidas.value = vidas;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) ///////////////////////////////////////// codi repetit?
    {
        if (collision.gameObject.CompareTag("elevator"))
        {
            transform.parent = collision.gameObject.transform;
        }

        if (collision.gameObject.CompareTag("Deathzone"))
        {
            LoseLife(10);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("elevator"))
        {
            transform.parent = null;
        }
    }
    public void AplicarGolpe ()
    {
        puedeMoverse = false;

        Vector2 direccion;

        if (body.velocity.x > 0)
        {
            direccion = new Vector2(-1, 1);
            body.AddForce(direccion * fuerzaGolpe);
        }
        else
        {
            direccion = new Vector2(1,1);
            body.AddForce(direccion * fuerzaGolpe);
        }

        StartCoroutine (EsperarYActivarMovimiento());    
    }
    IEnumerator EsperarYActivarMovimiento ()
    {
        //DA TIEMPO A QUE EL PERSONAJE SALGA VOLANDO Y NO SE ACTIVE EL MOVIMIENTO AL INSTANTE
        yield return new WaitForSeconds(0.4f);
        puedeMoverse = true;
        LoseLife(10);
    }
    private IEnumerator Dash(float dashingTime, float dashingCooldown, float dashingPower)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        tr.emitting = true;
        body.velocity = GetComponent<SpriteRenderer>().flipX == true ? new Vector2( 1 * dashingPower, 0.1f) : 
                                                                       new Vector2(-1 * dashingPower, 0.1f);

        CanGetDamaged = false;

        yield return new WaitForSeconds(dashingTime * 1.1f);
        body.gravityScale = originalGravity;
        isDashing = false;
        tr.emitting = false;
        CanGetDamaged = true;

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

