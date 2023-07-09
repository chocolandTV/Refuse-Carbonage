using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceManager : MonoBehaviour
{
    public static RessourceManager Instance { get; private set; }
    private int Ressource_Income = 1;
    private int Ressource_Wallet = 10;
    
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
    public void CollectIncome()
    {
        Ressource_Wallet += Ressource_Income;
    }
    public void AddIncome(int value)
    {
        Ressource_Income += value;
    }
    public void RemoveIncome(int value)
    {
        Ressource_Income -= value;
    }
    public void AddWallet(int value) // LEVEL
    {
        Ressource_Wallet += value;
    }
    public void RemoveWallet(int value)// buy units
    {
        Ressource_Wallet-= value;
    }
    public bool UnitBuyable(int value)
    {
        return (Ressource_Wallet>= value);
    }
    public string getWallet()
    {
        return Ressource_Wallet.ToString();
    }
    public string getIncome()
    {
        return Ressource_Income.ToString();
    }
}
