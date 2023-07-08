using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void OnButtonClick(int index)
    {
        // CHECK MINERAL
        WaveManager.Instance.AddUnit(index);
        

    }
}


