using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private GameObject[] buildings;
    [SerializeField] private GameObject[] SpawnPoints;
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

    public void EnemyTurn(int wave)
    {
        if (Random.Range(0, 100) < 70)
        {
            Spawn(buildings[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave);
        }
        if (Random.Range(0, 100) < 70)
        {
            Spawn(buildings[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave);
        }
        if (Random.Range(0, 100) < 70)
        {
            Spawn(buildings[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave);
        }
        if (Random.Range(0, 100) < 70)
        {
            Spawn(buildings[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave);
        }
        if (Random.Range(0, 100) < 70)
        {
            Spawn(buildings[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave);
        }
        if (Random.Range(0, 100) < 70)
        {
            Spawn(buildings[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave);
        }
        if (Random.Range(0, 100) < 70)
        {
            Spawn(buildings[0], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave);
        }
        if (Random.Range(0, 100) < 20)
        {
            Spawn(buildings[1], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, wave);
        }
        if (Random.Range(0, 100) < 10 + wave)
        {
            Spawn(buildings[2], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave );
        }
    }
    private void Spawn(GameObject obj, Vector3 pos, int wave)
    {
        
        GameObject _obj = Instantiate(obj, pos, Quaternion.identity);
        SelectableUnit _objComponent = _obj.GetComponent<SelectableUnit>();
        // IF BUILDING IS TOWER 
        if(obj == buildings[2])
        {
            _objComponent.damage = 1 * wave;
        }
        // LIFE
            _objComponent.currentLife *= wave; 
        // RESSOURCE AMOUNT * WAVE
            _objComponent.RessourceAmount +=wave;
        // LEVEL * WAVE
            _objComponent.level +=wave;

        TargetManager.Instance.allTarget.Add(_obj);
    }
    private Vector3 GetSafePosition(Vector3 position)
    {
        Collider[] intersection = Physics.OverlapSphere(position, 5f);
        if (intersection.Length == 0)
        {
            return position;
        }
        else
        {
            return GetSafePosition(SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position);
        }
    }

    // AFTER WAVE CHANCE TO SPAWN SCRAP 70%
    // AFTER WAVE CHANCE TO SPAWN TURRET 10% * WAVE
    // AFTER WAVE CHANCE TO SPAWN OILJACK 20%
    // AFTER WAVE 100 BOSS APPEAR  !
}
