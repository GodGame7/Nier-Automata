using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Text[] Slot_Text;
    [SerializeField] Text[] SlotQuantity_Text;
    [SerializeField] Image[] Slot_Image;
    [SerializeField] GameObject Selected_Item;
    [SerializeField] GameObject Inventory_UI_ob;

    Vector3 MoveSelectedPoint = new Vector3(0, -85.7f, 0);
    Vector3 defalutItemPosition;
    Color Selected_BackColor = new Color(218, 212, 186, 255);
    Color InvenUI_BackColor = new Color(78, 76, 66, 255);

    private int InvenLength;
    public int ListNum = 0;

    public bool isActiveInven = false;

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
            Slot_Text[i].text = string.Format("{0}", PlayerData.instance.inven.Items[i].ItemName);

        }
        for (int i = InvenLength / 2; i < InvenLength; i++) // ������ ����
        {
            Slot_Text[i].text = string.Format("{0}", PlayerData.instance.inven.Items[i - (InvenLength / 2)].Quantity);
        }
        ColorSet();

    }

    public void InvenActive()
    {
        if (!isActiveInven //&& InvenLength != 0
            )
        {
            isActiveInven = true;
            Inventory_UI_ob.SetActive(true);
            UpdateUI();

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
        }
        ColorSet();
    }

    public void UpSelected()
    {
        if (ListNum > 0 && InvenLength <= ListNum)
        {
            ListNum--;
            Selected_Item.transform.position -= MoveSelectedPoint;
        }
        ColorSet();
    }

    public void ColorSet()
    {
        for (int i = 0; i < InvenLength; i++)
        {
            Slot_Text[i].color = Color.white;
            SlotQuantity_Text[i].color = Color.white;
            Slot_Image[i].color = InvenUI_BackColor;


        }
        Slot_Text[ListNum].color = Color.black;
        SlotQuantity_Text[ListNum].color = Color.black;
        Slot_Image[ListNum].color = Selected_BackColor;


    }
}
