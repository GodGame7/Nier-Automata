using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Text[] Slot;
    [SerializeField] ItemData[] ItemData;
    private void Update()
    {
        for (int i = 0; i < Slot.Length; i++)
        {
            Slot[i].text = string.Format("{0} , {1}", ItemData[i].ItemName, ItemData[i].CurrntItem);
        }
        //ÅøÆÁÀ» ¾ÆÀÌÅÛ °¹¼ö·Î ¹Ù²ãÁÙ¿¹Á¤
    }
}
