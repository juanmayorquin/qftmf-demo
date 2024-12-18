using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class MovementController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private PlayerControllers _playerControllers;
    private AudioSource _audioSource;
    [SerializeField] private Vector2 _direction;
    
    public float speedMovement;
    public float jumpForce;
    public float defaultJump;
    public float defaultSpeed;
    
    //Components for logic and Sprites
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;
    
    private Rigidbody2D _rb2D;

    private bool canJump;
    private void Awake()
    {
        _playerControllers = new();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

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
        _animator.SetBool("Move", _direction.x != 0);
        _animator.SetBool("Jump", !canJump); 
        
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
        if (canJump)  
        {
            _rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);  
            _audioSource.Play();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }
}

