using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RewardDetails : ScriptableObject
{
    
}

public class ItemDetails
{
    public string name;
    public int id;
    public int powerCost;
    public int unlockAtLevel;
    public string details;
}