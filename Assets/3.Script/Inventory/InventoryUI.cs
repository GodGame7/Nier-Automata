using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Text[] Slot;
    [SerializeField] ItemData ItemData;

    public void UpdateUI()
    {
        for (int i = 0; i < Slot.Length; i++) // �÷��̾� ������ ����Ʈ Count���� �ؼ� �ٲ��ַ� 
                
        {
            Slot[i].text = string.Format("{0} , {1}", ItemData.ItemName, ItemData.Quantity); // ItemData�� Player.instacne.inventory[i]�� ����
            Slot[i].color = Color.white;
        }
        //������ ������ ������ �ٲ��ٿ���
        
    }
}
