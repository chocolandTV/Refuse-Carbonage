using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI Text_Mission, Text_WaveNextUnitCount, Text_WaveIndex,
     Text_WaveTime, Text_Ressource_Income, Text_Ressource_Wallet, Text_Map_UnitInfo;
     [SerializeField]private GameObject Unit_Firebat,Unit_Healmii, Unit_Jabbagoo, Unit_Magning, Unit_Aolimo, Unit_Wallimus, Godzilli;
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
    // START TEXT UPDATES
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
            case 6:
                Text_Map_UnitInfo.text = text;
                break;

            default:
                Text_Mission.text = "Mission: Collect Scrap";
                break;
        }
    }
    // START UNLOCK SECTION
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
