using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cost Table", menuName = "Car Dealer/Cost Table", order = 1)]
public class CostTable : ScriptableObject
{
    [SerializeField] private List<int> costs;

    public List<int> Costs { get => costs; }
}
