using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float playTime = 10f;
    public bool isFinished;
    private float time;
    
    [SerializeField] private GameObject characterMenu;
    private bool _isMenuOpen = false;
    
    [SerializeField] private PlayerInput _playerInput;

    private PlayerControllers _playerControllers;

    private void Update()
    {
        time = Time.deltaTime;
        playTime -= time;

        if (playTime <= 0 && !isFinished)
        {
            //TODO: LOSER FEEDBACK
        }

        if (isFinished)
        {
            //TODO: WINNER FEEDBACK
        }
    }

    private void Awake()
    {
        _playerControllers = new PlayerControllers();
    }

    private void OnEnable()
    {
        _playerControllers.Enable();

        // Listen whe the ey is pressed
        _playerControllers.Player.Menu.performed += OpenMenu;

        // Listen whe the ey is released
        _playerControllers.Player.Menu.canceled += CloseMenu;

    }

    private void OnDisable()
    {
        _playerControllers.Disable();
        //unsubscribe to the events
        _playerControllers.Player.Menu.performed -= OpenMenu;
        _playerControllers.Player.Menu.canceled -= CloseMenu;
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Play()
    {
        Time.timeScale = 1;
    }

    //Open the menu when the player hold the key
    private void OpenMenu(InputAction.CallbackContext context)
    {
        if (!_isMenuOpen)
        {
            _isMenuOpen = true;
            Time.timeScale = 0.2f;
            characterMenu.SetActive(true);
        }
    }
    //close the menu when the player release the key
    private void CloseMenu(InputAction.CallbackContext context)
    {
        if (_isMenuOpen)
        {
            _isMenuOpen = false;
            Time.timeScale = 1;
            characterMenu.SetActive(false);
        }
    }
}
