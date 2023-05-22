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
    [SerializeField] GameObject Inventory_UI;
    [SerializeField] GameObject Selected_Item;
    Vector3 defalutItemPosition;
    Vector3 MoveSelectedPoint;
    private void Awake() //ó�� ��ġ (1���ڸ�) �� �Ҵ�
    {
        defalutItemPosition = Selected_Item.transform.position;
        MoveSelectedPoint = new Vector3(0, -10f, 0); //-10f�� �ӽ÷� �Ҵ�,���� ���濹��

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
                Inventory_UI.SetActive(true);

            }
            else
            {
                isActiveInven = false;
                Inventory_UI.SetActive(false);
            }
        }

        //�κ��丮 List���� n��° ���������� Ȯ���ؼ� ��� , Ű������ ���Ʒ��� I �κ����� , K �� L �Ʒ� J ���

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isActiveInven)
            {

                //Player.instance.inventory.RemoveItem(ListNum);
                Item.UseItem();
            }
        }

        if (Input.GetKeyDown(KeyCode.K)) 
        {
            if (ListNum >= 0  //&& ListNum <= Player.instance.inventory.Count  
                )
            {
                ListNum++;
                Selected_Item.transform.position += MoveSelectedPoint;
                //

            }


        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (ListNum > 0  //Player.instance.inventory.Count <= ListNum
                )
                ListNum--;
            Selected_Item.transform.position -= MoveSelectedPoint;
        }



    }
}
