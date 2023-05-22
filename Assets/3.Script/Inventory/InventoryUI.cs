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
        InvenLength = PlayerData.instance.inven.Items.Count;
        Selected_Item.transform.position = defalutItemPosition;
        ListNum = 0;
        for (int i = 0; i < InvenLength / 2; i++)  //������ �̸�

        {
            Slot[i].text = string.Format("{0}", PlayerData.instance.inven.Items[i]); 
        }
        for (int i = InvenLength / 2; i < InvenLength; i++) // ������ ����
        {
            Slot[i].text = string.Format("{0}", PlayerData.instance.inven.Items[i - (InvenLength / 2)].Quantity); 
        }
        ColorSet();

    }

    public void InvenActive()
    {
        if (!isActiveInven && InvenLength != 0)
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
        if (ListNum >= 0 && ListNum <= InvenLength)
        {
            ListNum++;
            Selected_Item.transform.position += MoveSelectedPoint;
            ColorSet();
        }
    }

    public void UpSelected()
    {
        if (ListNum > 0 && InvenLength <= ListNum)
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
