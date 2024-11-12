using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public int health = 3;
    [SerializeField] private GameController _gameController;
    
    [Space(3)]
    [Header("Characters Values")]
    public Character characterEnum;
    
    //Prefabs of Characters
    // public List<Sprite> characterPrefab = new List<Sprite>();
    public List<GameObject> characterGO = new List<GameObject>();
    
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
    
    [Header("Feedbacks and UI")]
    [SerializeField] private GameObject FeedbackNegativo;

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
        if (health <= 0)
        {
            StartCoroutine(ShowPanelAndSwitchScene(FeedbackNegativo));
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            health--;
            _gameController.Reload();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Fall"))
        {
            _gameController.Reload();
        }
    }

    private void UpdateCharacter()
    {
        switch (characterEnum)
        {
            case Character.Lyon:
                //Change the sprite
                // _spriteRenderer.sprite =  characterPrefab[0];
                characterGO[0].SetActive(true);
                characterGO[1].SetActive(false);
                characterGO[2].SetActive(false);
                characterGO[3].SetActive(false);

                _movementController._animator = characterGO[0].GetComponent<Animator>();
                _movementController._spriteRenderer =  characterGO[0].GetComponent<SpriteRenderer>();
                //set the size and offset to the default values
                DefaultCollider();
                
                _movementController.SetDefaultValues();
                _movementController.jumpForce = _jumpLyonForce;

                break;
            case Character.Fede:
                //Change the sprite
                // _spriteRenderer.sprite =  characterPrefab[1];
                characterGO[1].SetActive(true);
                characterGO[0].SetActive(false);
                characterGO[2].SetActive(false);
                characterGO[3].SetActive(false);

                _movementController._animator = characterGO[1].GetComponent<Animator>();
                _movementController._spriteRenderer =  characterGO[1].GetComponent<SpriteRenderer>();
                //set the size and offset to the default values
                DefaultCollider();

                _movementController.SetDefaultValues();

                break;
            case Character.Mario:
                //Change the sprite
                // _spriteRenderer.sprite =  characterPrefab[2];
                characterGO[2].SetActive(true);
                characterGO[1].SetActive(false);
                characterGO[0].SetActive(false);
                characterGO[3].SetActive(false);

                _movementController._animator = characterGO[2].GetComponent<Animator>();
                _movementController._spriteRenderer =  characterGO[2].GetComponent<SpriteRenderer>();
                //set the size and offset to the default values
                DefaultCollider();
                
                _movementController.SetDefaultValues();

                break;
            case Character.Tomas:
                //Change the sprite
                // _spriteRenderer.sprite =  characterPrefab[3];
                characterGO[3].SetActive(true);
                characterGO[1].SetActive(false);
                characterGO[2].SetActive(false);
                characterGO[0].SetActive(false);

                _movementController._animator = characterGO[3].GetComponent<Animator>();
                _movementController._spriteRenderer =  characterGO[3].GetComponent<SpriteRenderer>();
                //set the size and offset to the default values
                _capsuleCollider2D.size = capsuleSmallSize;
                _capsuleCollider2D.offset = capsuleSmallOffset;
                
                _movementController.SetDefaultValues();

                _movementController.speedMovement = _speedTomas;
                break;

            default:
                // _spriteRenderer.sprite =  characterPrefab[0];
                characterGO[0].SetActive(true);
                characterGO[1].SetActive(false);
                characterGO[2].SetActive(false);
                characterGO[3].SetActive(false);

                _movementController._animator = characterGO[0].GetComponent<Animator>();
                _movementController._spriteRenderer =  characterGO[0].GetComponent<SpriteRenderer>();
                //set the size and offset to the default values
                DefaultCollider();
                _movementController.SetDefaultValues();
                _movementController.jumpForce = _jumpLyonForce;
                break;
        }
    }

    //Set to default values the collider
    void DefaultCollider()
    {
        _capsuleCollider2D.size = _colliderDefaultSize;
        _capsuleCollider2D.offset = _colliderDefaultOffset;
    }
    IEnumerator ShowPanelAndSwitchScene(GameObject panel)
    {
        // Hacemos visible el panel
        panel.SetActive(true);
        
        // Esperamos 2 segundos
        yield return new WaitForSeconds(2f);
        // Ocultamos el panel
        panel.SetActive(false);
        // Cargamos la escena "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }

    #region buttons for call

    //Method for switch characters from UI buttons 
    public void Switch2Lyon()
    {
        characterEnum = Character.Lyon;
    }
    public void Switch2Fede()
    {
        characterEnum = Character.Fede;
    }
    public void Switch2Mario()
    {
        characterEnum = Character.Mario;
    }
    public void Switch2Tomas()
    {
        characterEnum = Character.Tomas;
    }

    #endregion
   
}
