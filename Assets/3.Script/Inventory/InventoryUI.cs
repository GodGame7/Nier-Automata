using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Text[] Slot;
    [SerializeField] ItemData ItemData;
    private int InvenLength;
    private void Awake()
    {
        //InvenLength = Player.instance.inventory.count;
    }
    public void UpdateUI()
    {
        for (int i = 0; i < Slot.Length / 2; i++) // InvenLength까지  //아이템 이름

        {
            Slot[i].text = string.Format("{0}", ItemData.ItemName); // ItemData를 Player.instacne.inventory[i]로 변경
            Slot[i].color = Color.white;
        }
        for (int i = Slot.Length / 2; i < Slot.Length; i++) // 아이템 수량
        {
            Slot[i].text = string.Format("{0}", ItemData.Quantity); // player.instance.inventory[i-InvenLength] 로 변경
        }
        //툴팁을 아이템 갯수로 바꿔줄예정

    }
}
