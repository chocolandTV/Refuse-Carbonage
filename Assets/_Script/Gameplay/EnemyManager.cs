using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private GameObject[] buildings;
    private readonly float bounds = 42.0f;
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
  
    private Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(
            Random.Range(-bounds, bounds),
            25,
            Random.Range(-bounds, bounds)
    );
    }
    public void EnemyTurn(int wave)
    {
        
        if (Random.Range(0, 100) < 25)
        {
            Spawn(buildings[0], GetRandomSpawnPoint(),wave);
        }
        if (Random.Range(0, 100) < 10)
        {
            Spawn(buildings[1],  GetRandomSpawnPoint(), wave);
        }
        if (Random.Range(0, 100) < 5)
        {
            Spawn(buildings[2],  GetRandomSpawnPoint(),wave );
            
        }
         
    }
    
    private void Spawn(GameObject obj, Vector3 pos, int wave)
    {
        
        GameObject _obj = Instantiate(obj, pos, Quaternion.identity);
        SelectableUnit _objComponent = _obj.GetComponentInChildren<SelectableUnit>();
        // IF BUILDING IS TOWER 
        if(obj == buildings[2])
        {
            _objComponent.damage *= wave;
        }
        // LIFE
            _objComponent.currentLife *=wave; 
            _objComponent.MaxLife *=wave; 
        // RESSOURCE AMOUNT * WAVE
            _objComponent.RessourceAmount += wave;
        // LEVEL * WAVE
            _objComponent.level =_objComponent.damage *wave;

        //TargetManager.Instance.allTarget.Add(_obj);
    }
    
}
