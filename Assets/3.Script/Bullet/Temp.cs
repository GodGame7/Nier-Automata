using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{




    //  ----------------------  ��ǲ �Ŵ��� �� �������� --------------------------------------

    [SerializeField] private InventoryUI Inventory_UI;
    public delegate void Item(int num);
    public static event Item UseItem;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // ���ʾƷ� ���� �κ����� ����
        {
            Inventory_UI.InvenActive();
        }

        //�κ��丮 List���� n��° ���������� Ȯ���ؼ� ��� , Ű������ ���Ʒ��� I �κ����� , K �� L �Ʒ� J ���

        if (Inventory_UI.isActiveInven)
        {

            if (Input.GetKeyDown(KeyCode.J))
            {


                UseItem?.Invoke(Inventory_UI.ListNum);
                Inventory_UI.UpdateUI();


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
}
