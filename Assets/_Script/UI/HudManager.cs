using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }
    private bool[] unitUnlocked = new bool[9];
    [SerializeField]
    private TextMeshProUGUI Text_Mission, Text_WaveNextUnitCount, Text_WaveIndex,
     Text_WaveTime, Text_Ressource_Income, Text_Ressource_Wallet, Map_Name, Map_Damage, Map_Life, Map_RessourceAmount, Map_UnitInfo;
    [SerializeField] private GameObject Lock_unit_02, Lock_unit_03, Lock_unit_04, Lock_unit_05, Lock_unit_06, Lock_unit_07, Lock_unit_08;
    [SerializeField]private GameObject gameWinPanel;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < unitUnlocked.Length; i++)
        {

            unitUnlocked[i] = false;

        }
        unitUnlocked[0] = true;
        unitUnlocked[1] = true;
    }
    // WIN GAME PANEL
    public void GameWin()
    {
        // WHEN PLAYER WINS 
        GameWin_PanelSet(true);
    }
    public void OnButtonContinue()
    {
        GameWin_PanelSet (false);
        // NEXT LEVEL HARD MODE
    }
    private void GameWin_PanelSet(bool value)
    {
        gameWinPanel.SetActive(value);
    }
    // START TEXT UPDATES
    public bool getUnlockedUnitState(int index)
    {
        return unitUnlocked[index];
    }
    public void UnlockUnit(int wave)
    {
        // CHECK WAVE
        // UNLOCK ONLY 1 TIME
        if (wave >= 10 && !unitUnlocked[2])
        {
            Lock_unit_02.SetActive(false);
            unitUnlocked[2] = true;
        }
        if (wave >= 20 && !unitUnlocked[3])
        {
            Lock_unit_03.SetActive(false);
            unitUnlocked[3] = true;
        }
        if (wave >= 30 && !unitUnlocked[4])
        {
            Lock_unit_04.SetActive(false);
            unitUnlocked[4] = true;
        }
        if (wave >= 40 && !unitUnlocked[5])
        {
            Lock_unit_05.SetActive(false);
            unitUnlocked[5] = true;
        }
        if (wave >= 60 && !unitUnlocked[6])
        {
            Lock_unit_06.SetActive(false);
            unitUnlocked[6] = true;
        }
        if (wave >= 80 && !unitUnlocked[7])
        {
            Lock_unit_07.SetActive(false);
            unitUnlocked[7] = true;
        }
        if (wave >= 100 && !unitUnlocked[8])
        {
            Lock_unit_08.SetActive(false);
            unitUnlocked[8] = true;
        }
    }
    public void UpdateUnitMapInfo(string _name, string _damage, string _life, string _ressourceAmount, string _infoText)
    {
        Map_Name.text = "Name: " + _name;
        Map_Damage.text = "Damage: " + _damage;
        Map_Life.text = "Life: " + _life;
        Map_RessourceAmount.text = "Ressource holding:" +_ressourceAmount;
        Map_UnitInfo.text = "I remember:" +_infoText;
    }
    public void UpdateHUD(int id, string text)
    {
        switch (id)
        {
            case 0:
                Text_Mission.text = text;
                break;
            case 1:
                Text_WaveNextUnitCount.text = text;
                break;
            case 2:
                Text_WaveIndex.text = text;
                break;
            case 3:
                Text_WaveTime.text = text;
                break;
            case 4:
                Text_Ressource_Income.text = text;
                break;
            case 5:
                Text_Ressource_Wallet.text = text;
                break;
            default:
                Text_Mission.text = "Mission: Collect Scrap";
                break;
        }
    }
    // START UNLOCK SECTION
    public void OnMenuButton()
    {
        // PAUSE GAME
        // SHOW MENU
    }
    private void UnlockUnits(int index)
    {
        switch (index)
        {
            case 0:
                //    
                break;
            case 1:
                // Text_WaveNextUnitCount.text = text;
                break;
            case 2:
                // Text_WaveIndex.text = text;
                break;
            case 3:
                // Text_WaveTime.text = text;
                break;
            case 4:
                // Text_Ressource_Income.text = text;
                break;
            case 5:
                // Text_Ressource_Wallet.text = text;
                break;
            case 6:
                // Text_Map_UnitInfo.text = text;
                break;

            default:
                // Text_Mission.text = "Mission: Collect Scrap";
                break;
        }
    }
}
