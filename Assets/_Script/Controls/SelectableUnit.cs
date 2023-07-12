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
    public int UnitFraction = 0;
    public bool isHealed = false; // 0 ENEMY 1 PLAYER
    private int _healingSteps = 0;
    [SerializeField] private GameObject _HealthbarObject;
    [SerializeField] private Image _HealthbarSprite;
    private Camera _cam;
    private bool _healthbarActive=false;
    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        MaxLife = currentLife;
        
    }
    private void Start() {
        _cam = Camera.main;
    }
    private void Update() {
        if(_healthbarActive)
        {
            _HealthbarSprite.fillAmount = currentLife / MaxLife;
            // COLOR GREEN
            _HealthbarSprite.color = Color.green;
            if(currentLife / MaxLife < 0.76)
            {
                // COLOR YELLOW
                _HealthbarSprite.color = Color.yellow;
                if(currentLife / MaxLife < 0.15)
                {
                    _HealthbarSprite.color = Color.red;
                    // COLOR RED
                    
                }
            }
        }
    }
    public void AddLife(int value)
    {
        MaxLife +=value;
        currentLife += value;
        level = value;
    }
    public bool isValideToHeal()
    {
        return (currentLife < MaxLife  && !isHealed && UnitFraction ==1);
    }
   
    public void HealingOverTime(int amount)
    {
        isHealed = true;
        _healingSteps = amount;
    }
    private void UpdateHealthBar(bool _isActive, int _currentLife, int _maxLife)
    {
        if(_isActive)
        {
            _healthbarActive = true;
            // SET CAMERA
             _HealthbarObject.transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
            // TURN ON, SET VALUES AND COLOR
            _HealthbarObject.SetActive(true);
            _HealthbarSprite.fillAmount = (float) _currentLife /  _maxLife;
            Debug.Log((float) _currentLife /  _maxLife);
            // COLOR GREEN
            _HealthbarSprite.color = Color.green;
            if(_HealthbarSprite.fillAmount < 0.76)
            {
                // COLOR YELLOW
                _HealthbarSprite.color = Color.yellow;
                if(_HealthbarSprite.fillAmount < 0.15)
                {
                    _HealthbarSprite.color = Color.red;
                    // COLOR RED
                    
                }
            }
        }
        else{
            _healthbarActive = false;
            // TURN OFF 
            _HealthbarObject.SetActive(false);
        }
    }
    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
        // HudManager.Instance.UpdateHUD(6,infoText);
        HudManager.Instance.UpdateUnitMapInfo(unitName,damage.ToString(),currentLife.ToString(), RessourceAmount.ToString(), infoText);
        
        UpdateHealthBar(true,currentLife,MaxLife);

        if (gameObject.CompareTag("Enemy"))
        {

            TargetManager.Instance.UpdateTarget(gameObject.transform.position);
            
        }
    }
    public void OnDeselect()
    {
        SelectionSprite.gameObject.SetActive(false);
        UpdateHealthBar(false,currentLife,MaxLife);
    }
    
    public void Hit(int Amount)
    {
        
        currentLife -= Amount;
        // HEALING EFFECT
        if(isHealed)
        {
            currentLife+= _healingSteps;
            _healingSteps --;
            if(_healingSteps <= 0)
            {
                isHealed = false;
            }
        }
        // IF SELECTED THEN UPDATE HEALTHBAR
        if(SelectionManager.Instance.IsSelected(this))
        {
            UpdateHealthBar(true,currentLife,MaxLife);
            HudManager.Instance.UpdateUnitMapInfo(unitName,damage.ToString(),currentLife.ToString(), RessourceAmount.ToString(), infoText);
        
        }

        if (currentLife < 0)
        {
            if(UnitFraction == 1)
            {
                SoundManager.Instance.PlaySound(SoundManager.Sound.UnitDying, transform.position);
                TargetManager.Instance.currentUnits.Remove(gameObject);
            }
            if(UnitFraction ==0)
            {
                RessourceManager.Instance.AddIncome(RessourceAmount * WaveManager.Instance.CurrentWave);
                HudManager.Instance.UpdateHUD(4,RessourceManager.Instance.getIncome());
                
            }
            if(damage >= 1)
            {
                RessourceManager.Instance.STATS_TowerDestroyed++;
            }
            SelectionManager.Instance.Deselect(this);
            Destroy(gameObject);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (damage > 0 && other.CompareTag("Player") && UnitFraction == 0)
        {
            other.GetComponent<SelectableUnit>().Hit(damage);
            // GetComponent<TowerAttack>().AttackUnit(other.gameObject.transform.position);
        }
        if (other.CompareTag("Player") && !gameObject.CompareTag("Player") && UnitFraction == 0)
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
        if (damage > 0 && other.CompareTag("Player") && UnitFraction == 0)
        {
            other.GetComponent<SelectableUnit>().Hit(damage);
            // TOWER EFFECT 
            GetComponentInChildren<Animator>().SetTrigger("dealDamage");

        }
        if (other.CompareTag("Player") && !gameObject.CompareTag("Player") && UnitFraction == 0)
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
