using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed              // Velocitat
    { get { return _speed; } private set { } }

    [SerializeField] private float _speed;              
    [SerializeField] private int _maxJumpCount;      // Quantitat màxima de salts
    [SerializeField] private float _jumpHeight;
    [SerializeField] private LayerMask _groundLayer; // Detecció de col·lisió amb el ground 

    private BoxCollider2D _boxCollider; // La caixa de col·lisió del personatge
    private bool _dashing;              // Comprovar si està dasheant o no
    private int _counter;               // Conta els fotogrames que dura el dash
    private static Rigidbody2D _body;   
    private Animator _animator;         
    private int _jumpCount;             
    private bool _canJump;              
    private bool _isJumping;            
    private bool _movingLR;             // Decideix si l'sprite ha de fer flip en eix x o y (si està mirant dreta o esq)
    private Vector3 _initialPosition;   
    private PhysicsMaterial2D _defaultMaterial, _noFrictionMaterial; /* Material default i material sense
                                                                       fricció (no es queda enganxat a les parets) */
    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _jumpCount = 0;
        _initialPosition = _body.transform.position;

        _boxCollider = GetComponent<BoxCollider2D>();
        _defaultMaterial = GetComponent<PhysicsMaterial2D>();
        _noFrictionMaterial = new PhysicsMaterial2D();
        _noFrictionMaterial.friction = 0;
        _dashing = false;
        _counter = 0;
    }
    private void Update()
    {
        #region Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        _body.velocity = new Vector2(horizontalInput * Speed, _body.velocity.y);

        if (horizontalInput < 0)
        {
            _body.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontalInput > 0)
        {
            _body.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            _movingLR = true;
        }
        else
        {
            _movingLR = false;
            if (_body.velocity.x != 0 && !_isJumping)
            {
                _body.velocity = new Vector2(_body.velocity.x * 0.1f, _body.velocity.y);
            }
        }
        #endregion

        #region Jump
        _canJump = CanJump();
        if (Input.GetKeyDown(KeyCode.Space) && _canJump)
        {
            Jump();
        }
        #endregion

        #region Dash
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dashing = true;
        }

        if (_dashing)
        {

            if (_body.GetComponent<SpriteRenderer>().flipX)
            {
                _body.AddForce(new Vector2(1000f, 0));
            }
            else
            {
                _body.AddForce(new Vector2(-1000f, 0));
            }
            
            _counter++;
        }

        if (_counter == 50)
        {
            _dashing = false;
            _counter = 0;
        }
        #endregion

        // Respawn when falling
        if (_body.transform.position.y < -5)
        {
            _body.transform.position = _initialPosition;
        }
        _animator.SetBool("run", horizontalInput != 0 && _movingLR);
        _animator.SetBool("canJump", _isJumping);
    }
    private void Jump()
    {
        _jumpCount++;
        _body.velocity = new Vector2(_body.velocity.x, _jumpHeight * 2);
    }
    private bool CanJump()
    {
        RaycastHit2D hit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down, 0.1f, _groundLayer);

        if (hit.collider != null) // Si NO està a l'aire
        {
            _body.GetComponent<Rigidbody2D>().sharedMaterial = _defaultMaterial;
            _jumpCount = 0;
            _isJumping = false;
            return true;
        }
        else                      // Si ESTÀ a l'aire
        {
            _body.GetComponent<Rigidbody2D>().sharedMaterial = _noFrictionMaterial;
            _isJumping = true;

            if (_jumpCount < _maxJumpCount - 1)
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

