using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Text[] Slot;
    [SerializeField] ItemData[] ItemData;

    public void UpdateUI()
    {
        for (int i = 0; i < Slot.Length; i++) // 플레이어 아이템 리스트 Count까지 해서 바꿔주렴
        {
            Slot[i].text = string.Format("{0} , {1}", ItemData[i].ItemName, ItemData[i].Quantity);
        }
        //툴팁을 아이템 갯수로 바꿔줄예정
    }
}
