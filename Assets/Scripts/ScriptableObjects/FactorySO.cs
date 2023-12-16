using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObject",menuName = "ScriptableObject/FactorySO")]
public class FactorySO : ScriptableObject
{
    public int Level;
    public int ProductionRate;
    public int UpgradeCost;
    private int gemsProduced;
    private float lastProductionTime;
}
