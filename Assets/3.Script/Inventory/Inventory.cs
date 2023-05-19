using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemData> Inven = new List<ItemData>();

    public void AddItem(ItemData item) //������ ȹ���
    {
        int itemIndex = Inven.FindIndex(x => x.CurrntItem == item.CurrntItem);

        if(itemIndex <0) //�������� ���ٸ�
        {
            Inven.Add(item);
        }
        else //�������� �ִٸ�
        {
            Inven[itemIndex].CurrntItem++;
        }
    }
    public int GetItemCount(ItemData item) //�������� ������ �������� ���� �޼���
    {
        int itemIndex = Inven.FindIndex(x => x.CurrntItem == item.CurrntItem);

        if(itemIndex>=0) //�������� �ִٸ�
        {
            return Inven[itemIndex].CurrntItem;
        }
        else //�������� ���ٸ�
        {
            return 0;
        }
    }
    // List => HPS , HPM , HPL , HPS , HPS
    // HPS HPM HPL 
    // int itemIndex = Inven.FindIndex(HPS)
    // add -> if(itemIndex < 0) Inven.Add(HPS);
    // else Inven[itemIndex].����++;


}
