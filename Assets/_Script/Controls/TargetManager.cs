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
                Vector3 newPos = newTarget;
                newPos = Random.insideUnitCircle * 20;
                unit.GetComponent<NavMeshAgent>().SetDestination(newPos);
            }
        }
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
