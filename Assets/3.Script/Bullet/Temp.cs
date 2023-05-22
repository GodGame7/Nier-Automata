using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private Inventory inventory;
    private ItemEffect Item;

    private bool isActiveInven = false;
    private int ListNum = 0;

    //��ǲ �Ŵ���
    [SerializeField] GameObject Inventory_UI_ob;
    [SerializeField] GameObject Selected_Item;

    private InventoryUI Inventory_UI;
    Vector3 defalutItemPosition;
    private void Awake() //ó�� ��ġ (1���ڸ�) �� �Ҵ�
    {
        defalutItemPosition = Selected_Item.transform.position;

    }
    private void OnEnable() // Ȱ��ȭ�� �ɶ����� �� �Ҵ�� ��ġ�� �����ֵ���
    {
        Selected_Item.transform.position = defalutItemPosition;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // ���ʾƷ� ���� �κ����� ����
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

        //�κ��丮 List���� n��° ���������� Ȯ���ؼ� ��� , Ű������ ���Ʒ��� I �κ����� , K �� L �Ʒ� J ���

        if (isActiveInven)
        {

            if (Input.GetKeyDown(KeyCode.J))
            {

                Item.UseItem(ListNum);

            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                Inventory_UI.UpSelected();


            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Inventory_UI.DownSelected();
            }

        }

    }
}
