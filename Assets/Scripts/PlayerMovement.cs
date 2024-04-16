using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed              // Velocitat
    { get { return speed; } private set { } }

    [SerializeField] private float speed;              
    [SerializeField] private int maxJumpCount;      // Quantitat màxima de salts
    [SerializeField] private float jumpHeight;
    [SerializeField] private LayerMask groundLayer; // Detecció de col·lisió amb el ground 

    private BoxCollider2D boxCollider; // La caixa de col·lisió del personatge
    private bool dashing;              // Comprovar si està dasheant o no
    private int counter;               // Conta els fotogrames que dura el dash
    private static Rigidbody2D body;   
    private Animator animator;         
    private int jumpCount;             
    private bool canJump;              
    private bool isJumping;            
    private bool movingLR;             // Decideix si l'sprite ha de fer flip en eix x o y (si està mirant dreta o esq)
    private Vector3 initialPosition;   
    private PhysicsMaterial2D defaultMaterial, noFrictionMaterial; /* Material default i material sense
                                                                       fricció (no es queda enganxat a les parets) */
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = 0;
        initialPosition = body.transform.position;

        boxCollider = GetComponent<BoxCollider2D>();
        defaultMaterial = GetComponent<PhysicsMaterial2D>();
        noFrictionMaterial = new PhysicsMaterial2D();
        noFrictionMaterial.friction = 0;
        dashing = false;
        counter = 0;
    }
    private void Update()
    {
        #region Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * Speed, body.velocity.y);

        if (horizontalInput < 0)
        {
            body.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontalInput > 0)
        {
            body.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            movingLR = true;
        }
        else
        {
            movingLR = false;
            if (body.velocity.x != 0 && !isJumping)
            {
                body.velocity = new Vector2(body.velocity.x * 0.1f, body.velocity.y);
            }
        }
        #endregion

        #region Jump
        canJump = CanJump();
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashing = true;
        }

        if (dashing)
        {

            if (body.GetComponent<SpriteRenderer>().flipX)
            {
                body.AddForce(new Vector2(1000f, 0));
            }
            else
            {
                body.AddForce(new Vector2(-1000f, 0));
            }
            
            counter++;
        }

        if (counter == 50)
        {
            dashing = false;
            counter = 0;
        }
        #endregion

        // Respawn when falling
        if (body.transform.position.y < -5)
        {
            body.transform.position = initialPosition;
        }
        animator.SetBool("run", horizontalInput != 0 && movingLR);
        animator.SetBool("canJump", isJumping);
    }
    private void Jump()
    {
        jumpCount++;
        body.velocity = new Vector2(body.velocity.x, jumpHeight * 2);
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

            if (jumpCount < maxJumpCount - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

