using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemData> Inven = new List<ItemData>();

    public void AddItem(ItemData item) //아이템 획득시
    {
        int itemIndex = Inven.FindIndex(x => x.CurrntItem == item.CurrntItem);

        if(itemIndex <0) //아이템이 없다면
        {
            Inven.Add(item);
        }
        else //아이템이 있다면
        {
            Inven[itemIndex].CurrntItem++;
        }
    }
    public int GetItemCount(ItemData item) //아이템의 갯수를 가져오기 위한 메서드
    {
        int itemIndex = Inven.FindIndex(x => x.CurrntItem == item.CurrntItem);

        if(itemIndex>=0) //아이템이 있다면
        {
            return Inven[itemIndex].CurrntItem;
        }
        else //아이템이 없다면
        {
            return 0;
        }
    }
    // List => HPS , HPM , HPL , HPS , HPS
    // HPS HPM HPL 
    // int itemIndex = Inven.FindIndex(HPS)
    // add -> if(itemIndex < 0) Inven.Add(HPS);
    // else Inven[itemIndex].수량++;


}
