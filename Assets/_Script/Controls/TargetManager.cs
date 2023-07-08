using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetManager : MonoBehaviour
{
    public static TargetManager Instance { get; private set; }
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
    public List<GameObject> currentUnits = new List<GameObject>();
    public List<GameObject> allTarget = new List<GameObject>();
    public void UpdateTarget(Vector3 newTarget)
    {
        WaveManager.Instance.TargetPosition = newTarget;
        foreach (GameObject unit in currentUnits)
        {
            if(unit != null)
            {

                unit.GetComponent<NavMeshAgent>().SetDestination(WaveManager.Instance.TargetPosition);
            }
        }
    }
    public void SearchNewTarget(GameObject obj)
    {
        RemoveBuilding(obj);
        // GameObject[] gos;
        // gos = GameObject.FindGameObjectsWithTag("Enemy");
        // GameObject closest = null;
        // float distance = Mathf.Infinity;
        
        // foreach (GameObject go in gos)
        // {
        //     Vector3 diff = go.transform.position - obj.transform.position;
        //     float curDistance = diff.sqrMagnitude;
        //     if (curDistance < distance)
        //     {
        //         closest = go;
        //         distance = curDistance;
        //     }
        // }
        // closest.GetComponent<SelectableUnit>().OnSelected();
        // Debug.Log("NextTarget :" + closest.name);
        
        // UpdateTarget(closest.transform.position);
        UpdateTarget(allTarget[0].transform.position);
    }
    public void AddNewBuilding(GameObject unit)
    {
        allTarget.Add(unit);
    }
    public void RemoveBuilding(GameObject unit)
    {
        allTarget.Remove(unit);
    }
}
