using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Text[] Slot;
    [SerializeField] ItemData[] Data;
    private void Update()
    {
        for (int i = 0; i < Slot.Length; i++)
        {
            Slot[i].text = string.Format("{0} , {1}", Data[i].ItemName, Data[i].Tooltip);
        }
        //������ ������ ������ �ٲ��ٿ����Դϴ�
    }
}
