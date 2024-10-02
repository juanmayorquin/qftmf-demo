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

        // Escucha cuando se presiona la tecla
        _playerControllers.Player.Menu.performed += OpenMenu;

        // Escucha cuando se suelta la tecla
        _playerControllers.Player.Menu.canceled += CloseMenu;
    }

    private void OnDisable()
    {
        _playerControllers.Disable();

        _playerControllers.Player.Menu.performed -= OpenMenu;
        _playerControllers.Player.Menu.canceled -= CloseMenu;
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        if (!_isMenuOpen)
        {
            _isMenuOpen = true;
            Time.timeScale = 0.2f;
            characterMenu.SetActive(true);
        }
    }

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
