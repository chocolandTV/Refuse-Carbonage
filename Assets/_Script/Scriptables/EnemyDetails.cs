using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Data", menuName ="ScriptableObjects/BuildingDetails", order =1)]
public class EnemyDetails : ScriptableObject
{
    public string unitName;
    public float speed;
    public int maxHealth;
    
}
