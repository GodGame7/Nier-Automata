using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Item",menuName ="Item")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    [Header("����")]
    [SerializeField] public string Tooltip;
    [Header("����")]
    [SerializeField] private int HealingValue;

    
    //[Space]
    //[Header("Ĩ ī��Ʈ��")]
    //[SerializeField] private float ChipCount;
    //[Header("Ĩ ����")]
    //[SerializeField] private float Attack;
    //[SerializeField] private float Defence;
    //[SerializeField] private bool isAutoHeal;
    //[SerializeField] private bool isAutoFire;
    //[SerializeField] private bool isLifeSteal;



    
}
