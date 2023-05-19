using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    [Header("설명")]
    public string Tooltip;
    [Header("힐량")]
    public int HealingValue;
    [Header("갯수")]
    public int CurrntItem;
    [Header("아이템 넘버")]
    public int ItemNum;


    //[Space]
    //[Header("칩 카운트량")]
    //[SerializeField] private float ChipCount;
    //[Header("칩 스텟")]
    //[SerializeField] private float Attack;
    //[SerializeField] private float Defence;
    //[SerializeField] private bool isAutoHeal;
    //[SerializeField] private bool isAutoFire;
    //[SerializeField] private bool isLifeSteal;




}
