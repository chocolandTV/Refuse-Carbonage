using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class SelectableUnit : MonoBehaviour
{

    [SerializeField] private SpriteRenderer SelectionSprite;
    public string unitName = "EmptyUnit";
    public string infoText = "Text";
    public int RessourceAmount = 1;
    public int level = 1;
    public int damage = 1;
    public int currentLife = 10;
    public int MaxLife;
    public int UnitFaction = 0;
    public bool isHealed = false; // 0 ENEMY 1 PLAYER
    private int _healingSteps = 0;
    [SerializeField] private GameObject _HealthbarObject;
    [SerializeField]private GameObject _Foreground;
    private Image _healthbarImage;
    private Camera _cam;
    private bool _healthbarActive = false;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        
        _healthbarImage = _Foreground.GetComponent<Image>();

    }
    private void Start()
    {
        _cam = Camera.main;
        MaxLife = currentLife;
    }
    private void FixedUpdate()
    {
        if (_healthbarActive)
        {
            _healthbarImage.fillAmount = currentLife / MaxLife;
            // COLOR GREEN
            if(currentLife / MaxLife > 0.76)
            {
                _healthbarImage.color = Color.green;
            }
            else if (currentLife / MaxLife < 0.76 && currentLife / MaxLife > 0.15)
            {
                // COLOR YELLOW
                _healthbarImage.color = Color.yellow;
            }
            else if (currentLife / MaxLife < 0.15)
            {
                _healthbarImage.color = Color.red;
                // COLOR RED

            }
        }
    }
    public void AddLife(int value)
    {
        MaxLife *= value;
        currentLife *= value;
        level = value;
    }
    public void AddDamage(int value)
    {
        damage *= value;
        
    }
    public bool isValideToHeal()
    {
        return (currentLife < MaxLife && !isHealed && UnitFaction == 1);
    }

    public void HealingOverTime(int amount)
    {
        isHealed = true;
        _healingSteps = amount;
    }
    private void UpdateHealthBar(bool _isActive, int _currentLife, int _maxLife)
    {
        if (_isActive)
        {
            _healthbarActive = true;
            // SET CAMERA
            _HealthbarObject.transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
            // TURN ON, SET VALUES AND COLOR
            _HealthbarObject.SetActive(true);
            _healthbarImage.fillAmount = (float)_currentLife / _maxLife;
            
            // COLOR GREEN
            _healthbarImage.color = Color.green;
            if (_healthbarImage.fillAmount < 0.76)
            {
                // COLOR YELLOW
                _healthbarImage.color = Color.yellow;
                if (_healthbarImage.fillAmount < 0.15)
                {
                    _healthbarImage.color = Color.red;
                    // COLOR RED

                }
            }
        }
        else
        {
            _healthbarActive = false;
            // TURN OFF 
            _HealthbarObject.SetActive(false);
        }
    }
    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
        // HudManager.Instance.UpdateHUD(6,infoText);
        HudManager.Instance.UpdateUnitMapInfo(unitName, damage.ToString(), currentLife.ToString(), RessourceAmount.ToString(), infoText);

        UpdateHealthBar(true, currentLife, MaxLife);

        if (gameObject.CompareTag("Enemy"))
        {

            TargetManager.Instance.UpdateTarget(gameObject.transform.position);

        }
    }
    public void OnDeselect()
    {
        SelectionSprite.gameObject.SetActive(false);
        UpdateHealthBar(false, currentLife, MaxLife);
    }

    public void Hit(int Amount)
    {

        currentLife -= Amount;
        // HEALING EFFECT
        if (isHealed)
        {
            currentLife += _healingSteps;
            _healingSteps--;
            if (_healingSteps <= 0)
            {
                isHealed = false;
            }
        }
        // IF SELECTED THEN UPDATE HEALTHBAR
        if (SelectionManager.Instance.IsSelected(this))
        {
            UpdateHealthBar(true, currentLife, MaxLife);
            HudManager.Instance.UpdateUnitMapInfo(unitName, damage.ToString(), currentLife.ToString(), RessourceAmount.ToString(), infoText);

        }

        if (currentLife < 0)
        {
            if (UnitFaction == 1)
            {
                Debug.Log("Sound Played - Unit dying");
                SoundManager.Instance.PlaySound(SoundManager.Sound.UnitDying, transform.position);
                // Update HUD Count
                HudManager.Instance.UpdateHUD(3, "" + TargetManager.Instance.currentUnits.Count);
                TargetManager.Instance.currentUnits.Remove(gameObject);
            }
            if (UnitFaction == 0)
            {
                if (unitName == "Human Attack Tower")
                {
                    RessourceManager.Instance.STATS_TowerDestroyed++;
                }
                // Add missionUpdater
                if (unitName == "Human Scrap" && !WaveManager.Instance.isMissionDone[0])
                {
                    Debug.Log(" Mission 1 Tower Destroyed.");
                    WaveManager.Instance.isMissionDone[0] = true;
                    MissionManager.Instance.UpdateMission();
                }
                if (unitName == "Human OilJacky" && !WaveManager.Instance.isMissionDone[1])
                {
                    Debug.Log(" Mission 2 Tower Destroyed.");
                    WaveManager.Instance.isMissionDone[1] = true;
                    MissionManager.Instance.UpdateMission();
                }
                if (unitName == "Human Attack Tower" && !WaveManager.Instance.isMissionDone[2])
                {
                    Debug.Log(" Mission 5 Tower Destroyed.");
                    WaveManager.Instance.isMissionDone[2] = true;
                    MissionManager.Instance.UpdateMission();
                }
                if (unitName == "Human Attack Tower" && !WaveManager.Instance.isMissionDone[4] && RessourceManager.Instance.STATS_TowerDestroyed >= 10)
                {
                    
                    // IF TOWER 10
                    WaveManager.Instance.isMissionDone[3] = true;
                    MissionManager.Instance.UpdateMission();
                }
                if (unitName == "Easty" && WaveManager.Instance.CurrentWave > 1000 && !WaveManager.Instance.isMissionDone[5] && WaveManager.Instance.isMissionDone[0] && WaveManager.Instance.isMissionDone[1] && WaveManager.Instance.isMissionDone[2] && WaveManager.Instance.isMissionDone[3] && WaveManager.Instance.isMissionDone[4])
                {
                    Debug.Log(" Mission 100% EasterEgg found.");
                    // FINAL GAME 100%
                    // PARTICLE SYSTEM ON DEATH
                    WaveManager.Instance.isMissionDone[4] = true;
                    MissionManager.Instance.UpdateMission();
                }
                RessourceManager.Instance.AddIncome(RessourceAmount * WaveManager.Instance.CurrentWave);
                HudManager.Instance.UpdateHUD(4, RessourceManager.Instance.getIncome());

            }

            SelectionManager.Instance.Deselect(this);
            Destroy(gameObject);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (unitName == "Human Attack Tower")
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<SelectableUnit>().Hit(damage);
                
            }
        }
        if (other.CompareTag("Player") &&  UnitFaction == 0)
        {
            if (other.GetComponent<SelectableUnit>().damage > 0)
            {

                Hit(other.GetComponent<SelectableUnit>().damage);

            }
            else
            {
                if (level == 0)
                {
                    Hit(1);


                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (unitName == "Human Attack Tower")
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<SelectableUnit>().Hit(damage);
                // TOWER EFFECT 
                GetComponentInChildren<Animator>().SetTrigger("dealDamage");

            }

        }
        if (other.CompareTag("Player") && !gameObject.CompareTag("Player") && UnitFaction == 0)
        {
            if (other.GetComponent<SelectableUnit>().damage > 0)
            {
                Hit(other.GetComponent<SelectableUnit>().damage);


            }
            else
            {
                if (level == 0)
                {
                    Hit(1);


                }
            }
        }
    }
}
