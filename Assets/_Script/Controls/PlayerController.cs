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

    private void Start()
    {
        SubscribeToInput();

    }

    //////////////////////////////////
    // INPUT 
    //////////////////////////////////
    private void SubscribeToInput()
    {
        InputManager.OnLook += OnLookInput;
        InputManager.OnInteract += OnInteractInput;
        InputManager.OnSpawn += OnSpawnInput;
    }
    private void UnsubscribeToInput()
    {
        InputManager.OnLook -= OnLookInput;
        InputManager.OnInteract -= OnInteractInput;
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

            //_mousePositionStart = Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue());
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _unit = hit.transform.gameObject.GetComponent<SelectableUnit>();
                if (_unit != null && _unit.UnitFraction == 0)
                {
                    // SelectionManager.Instance.DeselectAll();
                    SelectionManager.Instance.Select(_unit);
                    TargetManager.Instance.UpdateTarget(hit.transform.position);

                }
                else if (_unit != null && _unit.UnitFraction == 1)
                {
                    // UPDATE MAP INFO
                    Debug.Log("Selected Unit: " + _unit.infoText);
                }
                else
                {
                    // SelectionManager.Instance.DeselectAll();  NO HIT
                }
            }

        }

    }
    public void OnSpawnInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // try parse the name of the control to an int
            int index = 11;
            int.TryParse(context.control.name, out index);
            if (index < 11)
            {
                WaveManager.Instance.AddUnit(index);
            }
            if (index == 11)
            {
                // START MENU HIDE HUD
            }
            // IF ENOUGH MINERAL
            // WAVE MANAGER  add UNIT [id index]

        }
    }
}
