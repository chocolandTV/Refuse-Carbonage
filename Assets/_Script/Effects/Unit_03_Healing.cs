using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_03_Healing : MonoBehaviour
{
    [SerializeField]private GameObject HealEffectObject;
    public int healingDamage = 3;
    private float TimeStep = 1f;
    private float time = 0;
    // Start is called before the first frame update
    
    private void HealUnit(GameObject target)
    {
       
        
            target.GetComponent<SelectableUnit>().currentLife += healingDamage;
            HealEffectObject.SetActive(true);
            Debug.Log("Healing");
            
    }
    private void OnTriggerStay(Collider other) {
        if(other.GetComponent<SelectableUnit>().isDamaged())
        {
            HealUnit(other.gameObject);
        }
    }    
        
    
}
