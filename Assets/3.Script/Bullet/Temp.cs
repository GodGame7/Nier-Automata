using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{




    //  ----------------------  ��ǲ �Ŵ��� �� �������� --------------------------------------

    private ItemEffect Item;
    [SerializeField] private InventoryUI Inventory_UI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // ���ʾƷ� ���� �κ����� ����
        {
            Inventory_UI.InvenActive();
        }

        //�κ��丮 List���� n��° ���������� Ȯ���ؼ� ��� , Ű������ ���Ʒ��� I �κ����� , K �� L �Ʒ� J ���

        //if (isActiveInven)
        //{

        if (Input.GetKeyDown(KeyCode.J))
        {

            Item.UseItem(Inventory_UI.ListNum);

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Inventory_UI.UpSelected();


        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Inventory_UI.DownSelected();
        }

        //}

    }
    // ---------------------------------------------------------------------------------------------
}
