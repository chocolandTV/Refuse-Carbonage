using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_03_Healing : MonoBehaviour
{
    [SerializeField]private GameObject HealEffectObject;
    public int healingDamage = 1;
    private float TimeStep = 0.5f;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(time > TimeStep)
        {
            time =0;
            HealUnit();
        }
        time += Time.fixedDeltaTime;
    }
    private void HealUnit()
    {
        // GET NEARST REFUSY WITH LESS LIFE
        
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in TargetManager.Instance.currentUnits)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && go.GetComponent<SelectableUnit>().currentLife < go.GetComponent<SelectableUnit>().MaxLife)
            { 
                closest = go;
                distance = curDistance;
            }
        }
        if(closest != null)
        {

            closest.GetComponent<SelectableUnit>().currentLife += healingDamage;
            HealEffectObject.SetActive(true);
            Debug.Log("Healing");
        }
        
        
    }
}
