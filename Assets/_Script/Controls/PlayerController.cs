using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject emptyTargetPrefab;
    private GameObject EmptyTarget;
    private Vector2 _lookInputDelta;
    
    private SelectableUnit _unit;
    private bool isSelected;
    private bool isButtonPressed = false;
    private int unitType = 0;
    private float SpawnCooldown = 0.025f;
    private float spawnTime = 0.025f;
    private Camera cam;
    
    private void Start()
    {
        SubscribeToInput();
        EmptyTarget = Instantiate(emptyTargetPrefab, Vector3.zero, Quaternion.identity);
        EmptyTarget.SetActive(false);
        isSelected = false;
        cam = Camera.main;
    }
    private void FixedUpdate()
    {
        if (isButtonPressed)
        {
            if (spawnTime > SpawnCooldown)
            {
                spawnTime = 0f;
                WaveManager.Instance.AddUnit(unitType);
            }
            spawnTime += Time.fixedDeltaTime;

        }
    }
    //////////////////////////////////
    // INPUT 
    //////////////////////////////////
    private void SubscribeToInput()
    {
        InputManager.OnLook += OnLookInput;
        InputManager.OnInteract += OnInteractInput;
        InputManager.OnSpawn += OnSpawnInput;
        InputManager.OnZoom += OnZoomInput;
        InputManager.OnPause += OnPauseInput;
    }
    private void UnsubscribeToInput()
    {
        InputManager.OnLook -= OnLookInput;
        InputManager.OnInteract -= OnInteractInput;
        InputManager.OnSpawn -= OnSpawnInput;
        InputManager.OnZoom -= OnZoomInput;
        InputManager.OnPause -= OnPauseInput;
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
    private void OnPauseInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!MenuManager.Instance.isGamePaused)
            {
                Debug.Log("Game Paused.");
                MenuManager.Instance.OnChangeGamePaused();
                MenuManager.Instance.isGamePaused = true;

            }
            else
            {
                Debug.Log("Game Resume");
                MenuManager.Instance.OnChangeGameResume();
                MenuManager.Instance.isGamePaused = false;
            }
        }
    }
    private void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            isSelected = false;
            EmptyTarget.SetActive(false);
            SelectionManager.Instance.DeselectAll();
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                isSelected = true;
                _unit = hit.transform.gameObject.GetComponent<SelectableUnit>();

                if (_unit != null && _unit.UnitFaction == 0)
                {
                    // Debug.Log("Selected Enemy"); 
                    // SelectionManager.Instance.DeselectAll();
                    SelectionManager.Instance.Select(_unit);
                    TargetManager.Instance.UpdateTarget(hit.transform.position);
                    if (Random.Range(0, 100) > 30)
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Sound.UnitAttack, _unit.transform.position);

                    }
                }
                else if (_unit != null && _unit.UnitFaction == 1)
                {
                    // Debug.Log("Selected Refusy"); 
                    // UPDATE MAP INFO
                    SelectionManager.Instance.Select(_unit);
                    HudManager.Instance.UpdateHUD(6, _unit.infoText);
                    if (Random.Range(0, 100) > 30)
                    {
                        SoundManager.Instance.PlaySound(SoundManager.Sound.UnitLaughing, _unit.transform.position);

                    }

                }
                else
                {
                    Vector3 pos = hit.point;
                    pos.y += 0.1f;
                    EmptyTarget.transform.position = pos;
                    EmptyTarget.SetActive(true);
                    // Debug.Log("Empty Targetposition: " + pos); 
                    HudManager.Instance.UpdateUnitMapInfo("No Unit Selected", "Your Refusys feeling well", "Tower Destroyed: " + RessourceManager.Instance.STATS_TowerDestroyed,
                     "Refusys created: " + RessourceManager.Instance.STATS_refusysSpawned, "Time played: " + (int)Time.time);

                    TargetManager.Instance.UpdateTarget(pos);
                    SoundManager.Instance.PlaySound(SoundManager.Sound.SetTarget, pos);
                }
            }

        }

    }
    public void OnSpawnInput(InputAction.CallbackContext context)
    {
        if (context.performed) // IF BUTTON IS HOLD
        {
            // try parse the name of the control to an int
            int index = 11;
            int.TryParse(context.control.name, out index);
            if (index < 11 && HudManager.Instance.getUnlockedUnitState(index))
            {
                // Debug.Log("isUnlocked- Unit:" + index);
                isButtonPressed = true;
                unitType = index;

            }
            // CHECK IF UNIT IS UNLOCKED


        }
        if (context.canceled)
        {
            isButtonPressed = false;
            spawnTime = 0.05f;
        }
    }

    public void OnZoomInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");           //This little peece of code is written by JelleWho https://github.com/jellewie
            if (ScrollWheelChange != 0)
            {                                            //If the scrollwheel has changed
                float R = ScrollWheelChange * 15;                                   //The radius from current camera
                float PosX = Camera.main.transform.eulerAngles.x + 90;              //Get up and down
                float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);       //Get left to right
                PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
                PosY = PosY / 180 * Mathf.PI;                                       //^
                float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
                float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
                float Y = R * Mathf.Cos(PosX);                                      //^
                float CamX = Camera.main.transform.position.x;                      //Get current camera postition for the offset
                float CamY = Camera.main.transform.position.y;                      //^
                float CamZ = Camera.main.transform.position.z;                      //^
                Camera.main.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
            }
        }
    }
}
