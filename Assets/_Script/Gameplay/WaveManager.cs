using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }
    public int CurrentWave => wave_index;
    [SerializeField] private List<GameObject> unitContainer = new List<GameObject>();
    [SerializeField] private GameObject firstTarget;
    private float wave_cooldown = 0;
    private float wave_time = 3;
    private int wave_index=0;
    public Vector3 TargetPosition{get;set;}
    private List<int> SpawningList = new List<int>();
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

    // Start is called before the first frame update
    void Start()
    {
        TargetPosition = firstTarget.transform.position;
        StartFirstWave();
        HudManager.Instance.UpdateHUD(3,""+ (int)(wave_time - wave_cooldown));
        HudManager.Instance.UpdateHUD(2,""+ wave_index);
        HudManager.Instance.UpdateHUD(1,"0");
        HudManager.Instance.UpdateHUD(5,RessourceManager.Instance.getWallet());
        HudManager.Instance.UpdateHUD(4,RessourceManager.Instance.getIncome());
    }

    // Update is called once per frame
    void Update()
    {
        if(wave_cooldown > wave_time)
        {
            wave_cooldown =0;
            //NEXT ROUND GET INCOME
            RessourceManager.Instance.CollectIncome();
            HudManager.Instance.UpdateHUD(5,RessourceManager.Instance.getWallet());
                ////////////////////////////////////////////////////////////////////
                // NEXT WAVE //
                ///////////////////////////////////////////////////////////////////
                wave_index++;
                if(wave_index ==100)
                {
                    HudManager.Instance.GameWin();
                }
                HudManager.Instance. UnlockUnit(wave_index);
                EnemyManager.Instance.EnemyTurn(wave_index);
                HudManager.Instance.UpdateHUD(2,""+ wave_index);

                // SPAWN WAVE
                SpawnUnits();
                SpawningList.Clear();
                HudManager.Instance.UpdateHUD(1,"0");
            
        }
        
        wave_cooldown +=Time.deltaTime;
        // HudManager.Instance.UpdateHUD(3,""+ (int)(wave_time - wave_cooldown));
    }
    private void StartFirstWave()
    {
        GameObject _spawn =Instantiate(unitContainer[0], transform.position, Quaternion.identity);
        _spawn.GetComponent<NavMeshAgent>().SetDestination(TargetPosition);
        TargetManager.Instance.currentUnits.Add(_spawn);
    }
    public void AddUnit(int index)
    {
        if(RessourceManager.Instance.UnitBuyable(index))
        {
            // Debug.Log("ADD UNIT:" + unitContainer[index].name);
            RessourceManager.Instance.RemoveWallet(index);
            SpawningList.Add(index);
            
            HudManager.Instance.UpdateHUD(4,RessourceManager.Instance.getIncome());
            HudManager.Instance.UpdateHUD(1,""+ SpawningList.Count);
            HudManager.Instance.UpdateHUD(5,RessourceManager.Instance.getWallet());
        }
                   
        //Debug.Log(" ADDED UNIT - " + unitContainer[index].name); 
    }
    private void SpawnUnits()
    {
        for (int i = 0; i < SpawningList.Count; i++)
        {
            GameObject _spawn =Instantiate(unitContainer[SpawningList[i]], transform.position, Quaternion.identity);
            RessourceManager.Instance.STATS_refusysSpawned++;
            Vector3 Pos = TargetPosition;
            Vector2 offset = Random.insideUnitCircle * 2f;
            Pos.x +=offset.x;
            Pos.z +=offset.y;
            _spawn.GetComponent<NavMeshAgent>().SetDestination(Pos);
            TargetManager.Instance.currentUnits.Add(_spawn);
            HudManager.Instance.UpdateHUD(3,""+ TargetManager.Instance.currentUnits.Count);
            
        }
        
    }
}
