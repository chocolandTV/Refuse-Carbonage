using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Data", menuName ="ScriptableObjects/BuildingDetails", order =1)]
public class EnemyDetails : ScriptableObject
{
    public string unitName = "EmptyUnit";
    public string infoText = "Text";
    public int RessourceAmount = 1;
    public int level = 1;
    public int damage = 1;
    public int MaxLife;
    public int UnitFraction = 0;
}
