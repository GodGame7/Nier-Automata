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
    Color Selected_BackColor = new Color(218 / 255f, 212 / 255f, 186 / 255f);
    Color InvenUI_BackColor = new Color(78 / 255f, 76 / 255f, 66 / 255f);

    private int MaxInvenLength = 3;
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

        InvenLength = PlayerData.Instance.inven.Items.Count; //�κ��丮�� ���� ĳ��
        if (InvenLength == 0) // �κ��丮�� ������ �κ��丮�� ������ �ʰ�
        {
            Inventory_UI_ob.SetActive(false);
            return;
        }
        Selected_Item.transform.position = defalutItemPosition; // �ٽ� Ȱ��ȭ�� ��ġ ������ �ø��� ����

        ListNum = 0; // ������ �ʱ�ȭ

        ClearSlot();

        for (int i = 0; i < InvenLength; i++)  //������ �̸�
        {
            Slot_Text[i].text = string.Format("{0}", PlayerData.Instance.inven.Items[i].ItemName);

        }
        for (int i = 0; i < InvenLength; i++) // ������ ����
        {
            SlotQuantity_Text[i].text = string.Format("{0}", PlayerData.Instance.inven.Items[i].Quantity);
        }

        ColorSet();

    }

    public void InvenActive() //�κ��丮�� ����,�ݴ� �޼���
    {
        if (!isActiveInven && InvenLength != 0
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
        if (ListNum >= 0 && ListNum < InvenLength - 1)
        {
            ListNum++;
            Selected_Item.transform.position += MoveSelectedPoint;
        }
        ColorSet();
    }

    public void UpSelected()
    {

        if (ListNum > 0 && ListNum < InvenLength)
        {
            ListNum--;
            Selected_Item.transform.position -= MoveSelectedPoint;
        }
        ColorSet();
    }

    public void ColorSet() // Ŀ�� �����Ͻ� ���� ��������.
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
    public void ClearSlot()  // �������� ����Ʈ���� ���ŵ����� ����� ����
    {
        for (int i = 0; i < MaxInvenLength; i++)
        {
            Slot_Text[i].text = "";
            SlotQuantity_Text[i].text = "";
            Slot_Image[i].color = InvenUI_BackColor;
        }
    }
}
