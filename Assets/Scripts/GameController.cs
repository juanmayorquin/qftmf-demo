using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject characterMenu;
    private bool _isMenuOpen = false;
    
    [SerializeField] private PlayerInput _playerInput;

    private PlayerControllers _playerControllers;

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
