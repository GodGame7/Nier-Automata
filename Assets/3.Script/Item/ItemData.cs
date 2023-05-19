using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    [Header("����")]
    public string Tooltip;
    [Header("����")]
    public int HealingValue;
    [Header("����")]
    public int CurrntItem;
    [Header("������ �ѹ�")]
    public int ItemNum;


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
