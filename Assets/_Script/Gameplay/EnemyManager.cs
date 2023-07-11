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
        if (Random.Range(0, 100) < 20)
        {
            Spawn(buildings[1], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position, wave);
        }
        if (Random.Range(0, 100) < 10 + wave)
        {
            Spawn(buildings[2], SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform.position,wave );
            SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingTower, transform.position);
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
            _objComponent.currentLife = wave * wave; 
        // RESSOURCE AMOUNT * WAVE
            _objComponent.RessourceAmount +=wave;
        // LEVEL * WAVE
            _objComponent.level =_objComponent.damage *wave;

        TargetManager.Instance.allTarget.Add(_obj);
    }
    
}
