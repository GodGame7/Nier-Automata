using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private Inventory inventory;

    private bool isActiveInven = false;


    //��ǲ �Ŵ���
    [SerializeField] GameObject Inventory_UI;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActiveInven)
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
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(isActiveInven)
            {
                //������ ���
            }
        }

    }
}
