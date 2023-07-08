using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    private Vector2 _lookInputDelta;
    private Vector3 _mousePositionStart;
    private Vector3 _mouisePositionEnds;
    private SelectableUnit _unit;
    private NavMeshAgent _agent;
    private void Start()
    {
        _agent =  GetComponent<NavMeshAgent>();
        _unit = GetComponent<SelectableUnit>();
        SubscribeToInput();
        SelectionManager.Instance.AvailableUnits.Add(_unit);
        // INITIAL TARGET
    }
    private void Move()
    {
        _agent.SetDestination(_mousePositionStart);
        
    }
    //////////////////////////////////
    // INPUT 
    //////////////////////////////////
    private void SubscribeToInput()
    {
        InputManager.OnLook+= OnLookInput;
        InputManager.OnInteract+= OnInteractInput;
        InputManager.OnSpawn += OnSpawnInput;
    }
    private void UnsubscribeToInput()
    {
        InputManager.OnLook-= OnLookInput;
        InputManager.OnInteract-= OnInteractInput;
        InputManager.OnSpawn -= OnSpawnInput;
    }
    private void OnLookInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lookInputDelta += context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _lookInputDelta = Vector2.zero;
        }
    }
    private void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _mousePositionStart = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
            if(SelectionManager.Instance.IsSelected(_unit))
            {
                // Move(); TO SELECTED UNITS PORT
            }
            // IF SELECTED UNIT is PLAYER MOVE  to mousePosition start as Destination with navMesh
        }
        else if (context.canceled)
        {
            _mouisePositionEnds = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());

            // ADD ALL ON PHYSICS COLLIDER TO SELECTED UNITS 
            // SelectionManager.Instance.AvailableUnits.Add(this);
        }
    }
    public void OnSpawnInput(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale != 0)
        {
            // try parse the name of the control to an int
            int index = 1;
            int.TryParse(context.control.name, out index);
            // IF ENOUGH MINERAL
            // WAVE MANAGER  add UNIT [id index]

        }
    }
}
