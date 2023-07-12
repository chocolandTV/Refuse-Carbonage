using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_03_Healing : MonoBehaviour
{
    [SerializeField]private GameObject HealEffectObject;
    public int healingDamage = 10;

    // Start is called before the first frame update
    
    private void HealUnit(GameObject target)
    {
       
        
            target.GetComponent<SelectableUnit>().HealingOverTime(healingDamage);
            HealEffectObject.SetActive(true);
           
            
    }
    private void OnTriggerStay(Collider other) {
        if(other.GetComponent<SelectableUnit>().isValideToHeal())
        {
            HealUnit(other.gameObject);
        }
    }    
        
    
}
