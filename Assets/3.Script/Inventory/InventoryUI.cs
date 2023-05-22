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

    Vector3 MoveSelectedPoint = new Vector3(0, -80f, 0); //-80f는 임시로 할당,추후 변경예정;
    Vector3 defalutItemPosition;

    private int InvenLength;
    public int ListNum = 0;

    private bool isActiveInven = false;

    private void Awake() //처음 위치 (1번자리) 값 할당
    {
        defalutItemPosition = Selected_Item.transform.position;

    }
    private void OnEnable() // 활성화가 될때마다 그 할당된 위치로 갈수있도록
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        Selected_Item.transform.position = defalutItemPosition;
        //InvenLength = Player.instance.inventory.count;
        ListNum = 0;
        for (int i = 0; i < InvenLength / 2; i++)  //아이템 이름

        {
            Slot[i].text = string.Format("{0}", ItemData.ItemName); // ItemData를 Player.instacne.inventory[i]로 변경
        }
        for (int i = InvenLength / 2; i < InvenLength; i++) // 아이템 수량
        {
            Slot[i].text = string.Format("{0}", ItemData.Quantity); // player.instance.inventory[i-InvenLength/2] 로 변경
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
