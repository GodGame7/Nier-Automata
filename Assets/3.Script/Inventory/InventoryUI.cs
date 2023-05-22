using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Text[] Slot;
    [SerializeField] ItemData ItemData;
    private int InvenLength;

    [SerializeField] GameObject Selected_Item;
    Vector3 MoveSelectedPoint = new Vector3(0, -80f, 0); //-80f�� �ӽ÷� �Ҵ�,���� ���濹��;
    private int ListNum=0;
    private void Awake()
    {
        //InvenLength = Player.instance.inventory.count;
    }
    public void UpdateUI()
    {
        for (int i = 0; i < Slot.Length / 2; i++) // InvenLength����  //������ �̸�

        {
            Slot[i].text = string.Format("{0}", ItemData.ItemName); // ItemData�� Player.instacne.inventory[i]�� ����
            Slot[i].color = Color.white;
        }
        for (int i = Slot.Length / 2; i < Slot.Length; i++) // ������ ����
        {
            Slot[i].text = string.Format("{0}", ItemData.Quantity); // player.instance.inventory[i-InvenLength] �� ����
        }
        //������ ������ ������ �ٲ��ٿ���

    }

    public void DownSelected()
    {
        if (ListNum >= 0  //&& ListNum <= Player.instance.inventory.Count  
                    )
        {
            ListNum++;
            Selected_Item.transform.position += MoveSelectedPoint;
            //

        }
    }

    public void UpSelected()
    {
        if (ListNum > 0  //Player.instance.inventory.Count <= ListNum
                    )
        {
            ListNum--;
            Selected_Item.transform.position -= MoveSelectedPoint;
        }
    }
}
