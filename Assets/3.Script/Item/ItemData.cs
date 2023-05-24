using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    public enum EItemType
    {
        HealingPotionSmall,
        HealingPotionMedium,
        HealingPotionLarge

    }

    public string ItemName;
    [Header("����")]
    public string Tooltip;
    [Header("����")]
    public int HealingValue;
    [Header("����")]
    public int Quantity;
    
    [Header("������ ����")] // ��� ���� ����;
    public int ItemNum;
    public EItemType ItemType;

    


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
