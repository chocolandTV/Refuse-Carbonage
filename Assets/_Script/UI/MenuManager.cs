using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject HudMenu;
    [SerializeField] private Slider SoundSlider;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider MouseSensitivitySlider;
    
    [SerializeField] private GameObject SettingMenuPanel; // ALLWAYS SWITCH TOO 
    [SerializeField] private GameObject CreditMenuPanel;
    [SerializeField] private GameObject SettingsPanel;

    private bool isGamePaused = false;
    public float CameraSpeedMultiplier = 0f;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {

        SoundSlider.onValueChanged.AddListener(delegate { OnChangeSoundVolume(); });
        MusicSlider.onValueChanged.AddListener(delegate { OnChangeMusicVolume(); });
        MouseSensitivitySlider.onValueChanged.AddListener(delegate { OnChangeMouseSensitivity(); });

    }
    private void OnPauseGameInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!isGamePaused)
            {
                OnChangeGamePaused();
                isGamePaused = true;
            }
            else
            {
                OnChangeGameResume();
                isGamePaused = false;
            }
        }
    }
    public void OnChangeSoundVolume()
    {
        // SoundManager.Instance.OnChangeSoundVolume(SoundSlider.value);
        Debug.Log(" CHANGE SoundsSlider TO : " + SoundSlider.value);
        //SoundManager.Instance.PlaySound(SoundManager.Sound.Robot_Happy, PlayerController.Instance.transform.position);
    }
    public void OnChangeMusicVolume()
    {
        //SoundManager.Instance.OnChangeMusicVolume(MusicSlider.value);
        Debug.Log(" CHANGE MusicSlider TO : " + MusicSlider.value);
    }

    public void OnChangeMouseSensitivity()
    {
        // fast 3.0f
        Debug.Log(" CHANGE MouseSlider TO : " + MouseSensitivitySlider.value);
        CameraSpeedMultiplier = MouseSensitivitySlider.value * 6;


    }
    public void OnChangeCredits()
    {
        // SETTINGS OFF
        SettingMenuPanel.SetActive(true);
        SettingsPanel.SetActive(false);
        CreditMenuPanel.SetActive(true);

        // CREDITS ON  - > BACK BUTTON
    }
    public void OnChangeSettings()
    {
        // SETTINGS OFF
        SettingMenuPanel.SetActive(true);
        SettingsPanel.SetActive(true);
        CreditMenuPanel.SetActive(false);

        // CREDITS ON  - > BACK BUTTON
    }
    public void OnChangeGameStart()
    {

        HudMenu.SetActive(true);
        MainMenu.SetActive(false);
        
        SceneManager.LoadScene("Game_Main");
        // UI OFF HUD ON
    }
    public void OnChangeGamePaused()
    {
        HudMenu.SetActive(false);
        MainMenu.SetActive(true);
        Time.timeScale = 0;

    }
    public void OnChangeGameResume()
    {
        HudMenu.SetActive(true);
        MainMenu.SetActive(false);
        Time.timeScale = 1;

    }
    public void OnChangeGameQuit()
    {
        Application.Quit();
    }
}