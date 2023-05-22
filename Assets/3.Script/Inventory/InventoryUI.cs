using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Text[] Slot;
    [SerializeField] ItemData ItemData;
    [SerializeField] GameObject Selected_Item;
    [SerializeField] GameObject Inventory_UI_ob;

    Vector3 MoveSelectedPoint = new Vector3(0, -80f, 0); //-80f�� �ӽ÷� �Ҵ�,���� ���濹��;
    Vector3 defalutItemPosition;

    private int InvenLength;
    public int ListNum = 0;

    private bool isActiveInven = false;

    private void Awake() //ó�� ��ġ (1���ڸ�) �� �Ҵ�
    {
        defalutItemPosition = Selected_Item.transform.position;

    }
    private void OnEnable() // Ȱ��ȭ�� �ɶ����� �� �Ҵ�� ��ġ�� �����ֵ���
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        Selected_Item.transform.position = defalutItemPosition;
        //InvenLength = Player.instance.inventory.count;
        ListNum = 0;
        for (int i = 0; i < InvenLength / 2; i++)  //������ �̸�

        {
            Slot[i].text = string.Format("{0}", ItemData.ItemName); // ItemData�� Player.instacne.inventory[i]�� ����
        }
        for (int i = InvenLength / 2; i < InvenLength; i++) // ������ ����
        {
            Slot[i].text = string.Format("{0}", ItemData.Quantity); // player.instance.inventory[i-InvenLength/2] �� ����
        }
        ColorSet();

    }

    public void InvenActive()
    {
        if (!isActiveInven //&& Player.instance.Inventory.count !=0
                               )
        {
            isActiveInven = true;
            Inventory_UI_ob.SetActive(true);

        }
        else
        {
            isActiveInven = false;
            Inventory_UI_ob.SetActive(false);
        }
    }

    public void DownSelected()
    {
        if (ListNum >= 0  //&& ListNum <= Player.instance.inventory.Count  
                    )
        {
            ListNum++;
            Selected_Item.transform.position += MoveSelectedPoint;
            ColorSet();
        }
    }

    public void UpSelected()
    {
        if (ListNum > 0  //Player.instance.inventory.Count <= ListNum
                    )
        {
            ListNum--;
            Selected_Item.transform.position -= MoveSelectedPoint;
            ColorSet();
        }
    }

    public void ColorSet()
    {
        for (int i = 0; i < InvenLength; i++)
        {
            Slot[i].color = Color.white;
        }
        Slot[ListNum].color = Color.black;
    }
}
