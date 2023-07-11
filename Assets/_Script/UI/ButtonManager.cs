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
        SoundManager.Instance.PlaySound(SoundManager.Sound.UIClick, transform.position);
        WaveManager.Instance.AddUnit(index);
        

    }
}


