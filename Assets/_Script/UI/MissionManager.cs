using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    [SerializeField] TextMeshProUGUI MissionText;
    [SerializeField] GameObject mission_check;
    private int currentMission = 0;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;   
    }
    private void Start()
    {
        Debug.Log("MissionManager Start");
        MissionText.text = "Destroy Scraps";
        mission_check.SetActive(false);
    }
    private string[] mission_texts = new string[10] {

   "Destroy Scraps", "Destroy Oiljacky", "Destroy Tower","Reach Wave 50", "Destroy 10 Towers","Reach Wave 100",
   "Reach Wave 250","Reach Wave 500","Reach Wave 1000", "Find Easter Egg"};
    IEnumerator CheckAndWait(int index)
    {
        mission_check.SetActive(true);
        yield return new WaitForSeconds(3);
        
        NextMissionStart(index);
        mission_check.SetActive(false);
    }
     void NextMissionStart(int index)
    {
        
        switch (index)
        {
           case 0:// Destroyed Scraps
                if(WaveManager.Instance.isMissionDone[0])
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 1 Destroy Scrap");
                }
                break;
            case 1:// Destroyed Oiljacky
                if(WaveManager.Instance.isMissionDone[1])
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 2 Destroy Oiljacky");
                }
                break;
            case 2://Destroyed Tower
                if(WaveManager.Instance.isMissionDone[2])
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 3 Destroy Tower");
                }
                break;
            case 3:
                if(WaveManager.Instance.CurrentWave >= 50)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 4 Wave 50");
                }
                break;
            case 4:
                if(WaveManager.Instance.isMissionDone[3])
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 5 Destroy 10 Towers");
                }
                break;
            case 5:
                if(WaveManager.Instance.CurrentWave >=100)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 6 Wave 100");
                }
                break;
            case 6:
                if(WaveManager.Instance.CurrentWave >=250)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 7 Wave 250");
                }
                break;
            case 7:
                if(WaveManager.Instance.CurrentWave >=500)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 8 Wave 500");
                }
                break;
            case 8:
                if(WaveManager.Instance.CurrentWave >=1000)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                    Debug.Log(" Mission 9 Wave 1000");
                }
                break;
            case 9:
                if(WaveManager.Instance.isMissionDone[4])
                {
                    currentMission++;
                    MissionText.text = "Easter Egg found, Game 100%";
                    Debug.Log(" Mission 100% EasterEgg found.");
                }
                break;
                                                                            
            default:
                MissionText.text = "Destroy Scraps";
                break;
        }
    }
    public void UpdateMission()
    {
        switch (currentMission)
        {
            case 0:// Destroyed Scraps
                if(WaveManager.Instance.isMissionDone[0])
                {
                    StartCoroutine(CheckAndWait(0));
                }
                break;
            case 1:// Destroyed Oiljacky
                if(WaveManager.Instance.isMissionDone[1])
                {
                    StartCoroutine(CheckAndWait(1));
                }
                break;
            case 2://Destroyed Tower
                if(WaveManager.Instance.isMissionDone[2])
                {
                   StartCoroutine(CheckAndWait(2));
                }
                break;
            case 3:
                if(WaveManager.Instance.CurrentWave >= 50)
                {
                     StartCoroutine(CheckAndWait(3));
                }
                break;
            case 4:
                if(WaveManager.Instance.isMissionDone[3])
                {
                    StartCoroutine(CheckAndWait(4));
                }
                break;
            case 5:
                if(WaveManager.Instance.CurrentWave >=100)
                {
                    StartCoroutine(CheckAndWait(5));
                }
                break;
            case 6:
                if(WaveManager.Instance.CurrentWave >=250)
                {
                    StartCoroutine(CheckAndWait(6));
                }
                break;
            case 7:
                if(WaveManager.Instance.CurrentWave >=500)
                {
                    StartCoroutine(CheckAndWait(7));
                }
                break;
            case 8:
                if(WaveManager.Instance.CurrentWave >=1000)
                {
                    StartCoroutine(CheckAndWait(8));
                }
                break;
            case 9:
                if(WaveManager.Instance.isMissionDone[4])
                {
                    StartCoroutine(CheckAndWait(9));
                }
                break;
                                                                            
            default:
                MissionText.text = "Destroy Scraps";
                break;
        }
    }
}
