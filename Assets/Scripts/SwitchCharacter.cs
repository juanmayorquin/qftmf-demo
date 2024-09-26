using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SwitchCharacter : MonoBehaviour
{
    public enum Character
    {
        Lyon = 0,
        Fede = 1,
        Mario = 2,
        Tomas = 3
    }

    [Header("Characters Values")]
    public Character characterEnum;
    
    //Prefabs of Characters
    public List<Sprite> characterPrefab = new List<Sprite>();

    private MovementController _movementController;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider2D;

    private Vector2 _colliderDefaultSize;
    private Vector2 _colliderDefaultOffset;
    
    [Header("Collider small values to switch")]
    [SerializeField] private Vector2 capsuleSmallSize;
    [SerializeField] private Vector2 capsuleSmallOffset;

    [Header("Lyon Stats")] 
    [SerializeField] private float _jumpLyonForce;
    // [Header("Fede Stats")] 
    // [Header("Mario Stats")] 
    [Header("Tomas Stats")] 
    [SerializeField] private float _speedTomas;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _movementController = GetComponent<MovementController>();

        _colliderDefaultSize = _capsuleCollider2D.size;
        _colliderDefaultOffset = _capsuleCollider2D.offset;
    }

    private void Update()
    {
        UpdateCharacter();
    }

    private void UpdateCharacter()
    {
        switch (characterEnum)
        {
            case Character.Lyon:
                //Change the sprite
                _spriteRenderer.sprite =  characterPrefab[0];
                //set the size and offset to the default values
                _capsuleCollider2D.size = _colliderDefaultSize;
                _capsuleCollider2D.offset = _colliderDefaultOffset;
                _movementController.SetDefaultValues();
                _movementController.jumpForce = _jumpLyonForce;

                break;
            case Character.Fede:
                //Change the sprite
                _spriteRenderer.sprite =  characterPrefab[1];
                //set the size and offset to the default values
                _capsuleCollider2D.size = _colliderDefaultSize;
                _capsuleCollider2D.offset = _colliderDefaultOffset;

                _movementController.SetDefaultValues();

                break;
            case Character.Mario:
                //Change the sprite
                _spriteRenderer.sprite =  characterPrefab[2];
                //set the size and offset to the default values
                _capsuleCollider2D.size = _colliderDefaultSize;
                _capsuleCollider2D.offset = _colliderDefaultOffset;
                
                _movementController.SetDefaultValues();

                break;
            case Character.Tomas:
                //Change the sprite
                _spriteRenderer.sprite =  characterPrefab[3];
                //set the size and offset to the default values
                _capsuleCollider2D.size = capsuleSmallSize;
                _capsuleCollider2D.offset = capsuleSmallOffset;
                
                _movementController.SetDefaultValues();

                _movementController.speedMovement = _speedTomas;
                break;

            default:
                _spriteRenderer.sprite =  characterPrefab[0];
                //set the size and offset to the default values
                _capsuleCollider2D.size = _colliderDefaultSize;
                _capsuleCollider2D.offset = _colliderDefaultOffset;
                _movementController.jumpForce = _jumpLyonForce;
                break;
        }
    }
}
