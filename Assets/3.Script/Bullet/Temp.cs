using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private Inventory inventory;

    


    //��ǲ �Ŵ���
    [SerializeField] GameObject Inventory_UI;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Inventory_UI.SetActive(true);
        }
        
    }
}
