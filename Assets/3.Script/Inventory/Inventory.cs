using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemData> Items = new List<ItemData>();
    public void AddItem(ItemData item) //������ ȹ���
    {
        int itemIndex = Items.FindIndex(x => x.CurrntItem == item.CurrntItem);

        if(itemIndex <0) //�������� ���ٸ� ������ ����
        {
            //Inven[0].Use();
            Items.Add(item);
        }
        else //�������� �ִٸ� ������ ���� �߰�
        {
            Items[itemIndex].CurrntItem++;
        }
    }
    public void RemoveItem(ItemData item)
    {
        int itemIndex = Items.FindIndex(x => x.CurrntItem == item.CurrntItem);
        if (itemIndex <= 1) //�������� 1�� ���Ͽ��� ����ϸ� ������ ����
        {
            Items.Remove(item);
        }
        else //�������� 2�� �̻��̶�� ������ ���� -1
        {
            Items[itemIndex].CurrntItem--;
        }
    }

    //public int GetItemCount(ItemData item) //�������� ������ �������� ���� �޼���
    //{
    //    int itemIndex = Items.FindIndex(x => x.CurrntItem == item.CurrntItem);

    //    if(itemIndex>=0) //�������� �ִٸ� ������ ������ŭ ��ȯ
    //    {
    //        return Items[itemIndex].CurrntItem;
    //    }
    //    else //�������� ���ٸ� 0�� ��ȯ
    //    {
    //        return 0;
    //    }
    //}
    // List => HPS , HPM , HPL , HPS , HPS
    // HPS HPM HPL 
    // int itemIndex = Inven.FindIndex(HPS)
    // add -> if(itemIndex < 0) Inven.Add(HPS);
    // else Inven[itemIndex].����++;


}
