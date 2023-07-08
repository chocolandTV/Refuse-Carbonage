using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectableUnit : MonoBehaviour
{

    [SerializeField] private SpriteRenderer SelectionSprite;
    [SerializeField] private ParticleSystem _particleSystem;
    public string infoText = "Text";
    public int RessourceAmount = 1;
    public int level = 1;
    public int damage = 1;
    public int life = 10;
    public int UnitFraction = 0; // 0 ENEMY 1 PLAYER

    private void Awake()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);


    }

    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
        HudManager.Instance.UpdateHUD(6,infoText);
        if (gameObject.CompareTag("Enemy"))
        {
            TargetManager.Instance.UpdateTarget(gameObject.transform.position);
            
        }
    }
    public void OnDeselect()
    {
        SelectionSprite.gameObject.SetActive(false);
        
    }
    public void Hit(int Amount)
    {
        life -= Amount;
        if (life < 0)
        {
            OnDeselect(); 
            HudManager.Instance.UpdateHUD(0, "Great, your Income increased + " + RessourceAmount);
            RessourceManager.Instance.AddIncome(RessourceAmount);
            HudManager.Instance.UpdateHUD(4,RessourceManager.Instance.getIncome());
            // PLAY SOUND 
            if (gameObject.CompareTag("Enemy"))
            {
                TargetManager.Instance.SearchNewTarget(gameObject);
            }
            if(UnitFraction ==1)
            {
                _particleSystem.Play();
            }
            Destroy(gameObject);

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (damage > 0 && other.CompareTag("Player") && UnitFraction == 0)
        {
            other.GetComponent<SelectableUnit>().Hit(damage);
            GetComponent<TowerAttack>().AttackUnit(other.gameObject.transform.position);
        }
        if (other.CompareTag("Player") && !gameObject.CompareTag("Player") && UnitFraction == 0)
        {
            if (other.GetComponent<SelectableUnit>().damage > 0)
            {
                Debug.Log("  FIGHTER DAMAGE");
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
            GetComponent<TowerAttack>().AttackUnit(other.gameObject.transform.position);

        }
        if (other.CompareTag("Player") && !gameObject.CompareTag("Player") && UnitFraction == 0)
        {
            if (other.GetComponent<SelectableUnit>().damage > 0)
            {
                Hit(other.GetComponent<SelectableUnit>().damage);
                Debug.Log("  FIGHTER DAMAGE");

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
