using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public int UnitFraction = 0; // 0 ENEMY 1 PLAYER

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        MaxLife = currentLife;

    }

    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
        // HudManager.Instance.UpdateHUD(6,infoText);
        HudManager.Instance.UpdateUnitMapInfo(unitName,damage.ToString(),currentLife.ToString(), RessourceAmount.ToString(), infoText);

        if (gameObject.CompareTag("Enemy"))
        {
            TargetManager.Instance.UpdateTarget(gameObject.transform.position);
            
        }
    }
    private void OnDestroy() {
        TargetManager.Instance.currentUnits.Remove(gameObject);
    }
    public void OnDeselect()
    {
        SelectionSprite.gameObject.SetActive(false);
        
    }
    public void Hit(int Amount)
    {
        currentLife -= Amount;
        if (currentLife < 0)
        {
            OnDeselect(); 
            HudManager.Instance.UpdateHUD(0, "Great, your Income increased + " + RessourceAmount);
            RessourceManager.Instance.AddIncome(RessourceAmount);
            HudManager.Instance.UpdateHUD(4,RessourceManager.Instance.getIncome());
            // PLAY SOUND 
            // if (gameObject.CompareTag("Enemy"))
            // {
            //     TargetManager.Instance.SearchNewTarget(gameObject);
            // }
            
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
}
