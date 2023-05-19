using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<ItemData> Items = new List<ItemData>();
    public void AddItem(ItemData item) //아이템 획득시
    {
        int itemIndex = Items.FindIndex(x => x.CurrntItem == item.CurrntItem);

        if(itemIndex <0) //아이템이 없다면 아이템 생성
        {
            //Inven[0].Use();
            Items.Add(item);
        }
        else //아이템이 있다면 아이템 갯수 추가
        {
            Items[itemIndex].CurrntItem++;
        }
    }
    public void RemoveItem(ItemData item)
    {
        int itemIndex = Items.FindIndex(x => x.CurrntItem == item.CurrntItem);
        if (itemIndex <= 1) //아이템이 1개 이하에서 사용하면 아이템 삭제
        {
            Items.Remove(item);
        }
        else //아이템이 2개 이상이라면 아이템 갯수 -1
        {
            Items[itemIndex].CurrntItem--;
        }
    }

    //public int GetItemCount(ItemData item) //아이템의 갯수를 가져오기 위한 메서드
    //{
    //    int itemIndex = Items.FindIndex(x => x.CurrntItem == item.CurrntItem);

    //    if(itemIndex>=0) //아이템이 있다면 아이템 갯수만큼 반환
    //    {
    //        return Items[itemIndex].CurrntItem;
    //    }
    //    else //아이템이 없다면 0을 반환
    //    {
    //        return 0;
    //    }
    //}
    // List => HPS , HPM , HPL , HPS , HPS
    // HPS HPM HPL 
    // int itemIndex = Inven.FindIndex(HPS)
    // add -> if(itemIndex < 0) Inven.Add(HPS);
    // else Inven[itemIndex].수량++;


}
