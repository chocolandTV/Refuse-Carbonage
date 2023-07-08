using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{
    [SerializeField] private float panSpeed = 20f;
    private float panSpeedOrigin = 20f;
    private Vector2 _moveInput;
    private void Start()
    {
        EnableCameraControl();
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 position = transform.position;

        if(_moveInput != Vector2.zero)
        {
            position.z+= _moveInput.y * panSpeed*Time.deltaTime;
            position.x+= _moveInput.x * panSpeed* Time.deltaTime;
            transform.position = position;
            panSpeed += 0.1f;
        }

        else{
            panSpeed = panSpeedOrigin;
        }
    }
    public void EnableCameraControl()
    {
        InputManager.OnMove += OnMoveInput;
    }
    public void DisableCameraControl()
    {
        InputManager.OnMove -= OnMoveInput;
    }

    ///////////////////////////////////////
    /// INPUT
    ///////////////////////////////////////
    private void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _moveInput = Vector2.zero;
        }
    }
}
