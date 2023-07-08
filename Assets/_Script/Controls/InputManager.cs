using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
using System;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerInput _playerInput;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _playerInput = GetComponent<PlayerInput>();
        SubscribeToInput();
        DontDestroyOnLoad(gameObject);
    }

    // EVENTS STATICS
    public static event Action<CallbackContext> OnLook;
    public static event Action<CallbackContext> OnMove;
    public static event Action<CallbackContext> OnInteract;
    public static event Action<CallbackContext> OnPause;
    public static event Action<CallbackContext> OnSpawn;

    private void OnLookInput(CallbackContext context)
    {
        OnLook?.Invoke(context);
    }
    private void OnMoveInput(CallbackContext context)
    {
        OnMove?.Invoke(context);
    }
    private void OnInteractInput(CallbackContext context)
    {
        OnInteract?.Invoke(context);
    }
    private void OnPauseInput(CallbackContext context)
    {
        OnPause?.Invoke(context);
    }
    private void OnSpawnInput(CallbackContext context)
    {
        OnSpawn?.Invoke(context);
    }

    ////////////////////////////////////////////////
    // SUBSCRIBE 
    ////////////////////////////////////////////////
    private void SubscribeToInput()
    {
        _playerInput.actions["Look"].started += OnLookInput;
        _playerInput.actions["Look"].performed += OnLookInput;
        _playerInput.actions["Look"].canceled += OnLookInput;

        _playerInput.actions["Move"].started += OnMoveInput;
        _playerInput.actions["Move"].performed += OnMoveInput;
        _playerInput.actions["Move"].canceled += OnMoveInput;

        _playerInput.actions["Interact"].started += OnInteractInput;
        _playerInput.actions["Interact"].performed += OnInteractInput;
        _playerInput.actions["Interact"].canceled += OnInteractInput;

        _playerInput.actions["Pause"].started += OnPauseInput;
        _playerInput.actions["Pause"].performed += OnPauseInput;
        _playerInput.actions["Pause"].canceled += OnPauseInput;

        _playerInput.actions["Spawn"].started += OnSpawnInput;
        _playerInput.actions["Spawn"].performed += OnSpawnInput;
        _playerInput.actions["Spawn"].canceled += OnSpawnInput;
    }
    ////////////////////////////////////////////////
    // UNSUBSCRIBE 
    ////////////////////////////////////////////////
    private void UnsubscribeToInput()
    {
        _playerInput.actions["Look"].started -= OnLookInput;
        _playerInput.actions["Look"].performed -= OnLookInput;
        _playerInput.actions["Look"].canceled -= OnLookInput;

        _playerInput.actions["Move"].started -= OnMoveInput;
        _playerInput.actions["Move"].performed -= OnMoveInput;
        _playerInput.actions["Move"].canceled -= OnMoveInput;

        _playerInput.actions["Interact"].started -= OnInteractInput;
        _playerInput.actions["Interact"].performed -= OnInteractInput;
        _playerInput.actions["Interact"].canceled -= OnInteractInput;

        _playerInput.actions["Pause"].started -= OnPauseInput;
        _playerInput.actions["Pause"].performed -= OnPauseInput;
        _playerInput.actions["Pause"].canceled -= OnPauseInput;

        _playerInput.actions["Spawn"].started -= OnSpawnInput;
        _playerInput.actions["Spawn"].performed -= OnSpawnInput;
        _playerInput.actions["Spawn"].canceled -= OnSpawnInput;
    }

    private void OnDestroy() {
        if(Instance ==this)
        {
            Instance = null;
            UnsubscribeToInput();
        }
    }
}
