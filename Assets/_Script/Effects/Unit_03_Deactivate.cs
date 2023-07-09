using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_03_Deactivate : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable() {
        StartCoroutine(waitSeconds());
    }
    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
        Debug.Log("Heal Deactivated");
    }
}
