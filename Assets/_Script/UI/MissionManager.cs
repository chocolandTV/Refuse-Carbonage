using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissionManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI MissionText;
    private int currentMission = 0;
    private void Start()
    {
        MissionText.text = "Destroy Scraps";
    }
    private string[] mission_texts = new string[10] {

   "Destroy Scraps", "Destroy Oiljacky", "Destroy Tower","Reach Wave 50", "Destroy 10 Towers","Reach Wave 100",
   "Reach Wave 250","Reach Wave 500","Reach Wave 1000", "Find Easter Egg"};
    public void UpdateMission()
    {
        switch (currentMission)
        {
            case 0:// Destroyed Scraps
                if(WaveManager.Instance.isMissionDone[0])
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 1:// Destroyed Oiljacky
                if(WaveManager.Instance.isMissionDone[1])
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 2://Destroyed Tower
                if(WaveManager.Instance.isMissionDone[2])
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 3:
                if(WaveManager.Instance.CurrentWave >= 50)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 4:
                if(WaveManager.Instance.isMissionDone[3])
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 5:
                if(WaveManager.Instance.CurrentWave >=100)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 6:
                if(WaveManager.Instance.CurrentWave >=250)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 7:
                if(WaveManager.Instance.CurrentWave >=500)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 8:
                if(WaveManager.Instance.CurrentWave >=1000)
                {
                    currentMission++;
                    MissionText.text = mission_texts[currentMission];
                }
                break;
            case 9:
                if(WaveManager.Instance.isMissionDone[4])
                {
                    currentMission++;
                    MissionText.text = "Easter Egg found, Game 100%";
                }
                break;
                                                                            
            default:
                MissionText.text = "Destroy Scraps";
                break;
        }
    }
}
