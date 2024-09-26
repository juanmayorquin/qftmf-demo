using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private PlayerControllers _playerControllers;
    [SerializeField] private Vector2 _direction;
    
    public float speedMovement;
    public float jumpForce;
    public float defaultJump;
    public float defaultSpeed;
    
    //Components for logic and Sprites
    [SerializeField] private GroundLogic _groundLogic;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private Rigidbody2D _rb2D;
    private void Awake()
    {
        _playerControllers = new();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();

        defaultJump = jumpForce;
        defaultSpeed = speedMovement;

    }

    private void OnEnable()
    {
        _playerControllers.Enable();

        _playerControllers.Player.Movement.started += Move;
        _playerControllers.Player.Movement.canceled += CancelMove;

        _playerControllers.Player.Jump.performed += Jump;
        
    }
    private void OnDisable()
    {
        _playerControllers.Disable();
        _playerControllers.Player.Movement.started -= Move;
        _playerControllers.Player.Movement.canceled -= CancelMove;

        _playerControllers.Player.Jump.performed -= Jump;
    }
    private void FixedUpdate()
    {
        // Se aplica el movimiento horizontal
        _rb2D.velocity = new Vector2(speedMovement * _direction.x, _rb2D.velocity.y);
        // _animator.SetBool("Move", _horizontal.x != 0);
        // _animator.SetBool("Jump", !_groundLogic.IsGrounded); 
        
        if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        
    }

    private void Move(InputAction.CallbackContext context)
    {
        // Se lee el valor del input
        _direction.x = context.ReadValue<float>();
    }
    private void CancelMove(InputAction.CallbackContext context)
    {
        // Se lee el valor del input
        _direction = Vector2.zero;
    }

    public void SetDefaultValues()
    {
        jumpForce = defaultJump;
        speedMovement = defaultSpeed;
    }
    private void Jump(InputAction.CallbackContext context)
    {
        //Se accede a la logica del script de GroundLogic, primero confirmando que este el objeto referenciado
        if (_groundLogic != null && _groundLogic.isGrounded)  
        {
            _rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);  
        }
        else
        {
            Debug.Log("No puedo saltar, no estoy en el suelo");
        }
    }
   
}

