using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public float playTime;
    public bool isFinished;
    private float time;
    private float totalTime;
    public bool level1;
    
    [SerializeField] private GameObject characterMenu;
    [SerializeField] private Image BarTimeImage;
    private bool _isMenuOpen = false;
    
    [SerializeField] private PlayerInput _playerInput;

    private PlayerControllers _playerControllers;
    
    [Header("Feedbacks and UI")]
    [SerializeField] private GameObject FeedbackNegativo;
    [SerializeField] private GameObject FeedbackPositivo;

    private void Update()
    {
        time = Time.deltaTime;
        playTime -= time;

        if (BarTimeImage != null) 
        {
            BarTimeImage.fillAmount = playTime / totalTime; 
        }
        
        if (playTime <= 0 && !isFinished)
        {
            StartCoroutine(ShowPanelAndSwitchScene(FeedbackNegativo, "MainMenu"));
        }

        if (isFinished && level1)
        {
            StartCoroutine(ShowPanelAndSwitchScene(FeedbackPositivo, "Level2"));
        }
        if (isFinished && !level1)
        {
            StartCoroutine(ShowPanelAndSwitchScene(FeedbackPositivo, "MainMenu"));
        }
    }

    private void Awake()
    {
        _playerControllers = new PlayerControllers();
    }

    private void Start()
    {
        totalTime = playTime;
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

    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator ShowPanelAndSwitchScene(GameObject panel, string NameScene)
    {
        // Hacemos visible el panel
        panel.SetActive(true);
        // Esperamos 2 segundos
        yield return new WaitForSeconds(2f);
        // Ocultamos el panel
        panel.SetActive(false);
        // Cargamos la escena "MainMenu"
        SceneManager.LoadScene(NameScene);
    }
}
