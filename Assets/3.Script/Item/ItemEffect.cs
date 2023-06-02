using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    [SerializeField] Temp temp;


    private InventoryUI Inven_UI;
    private void Awake()
    {

        Temp.UseItem += UseItem;


    }

    public void UseItem(int num)
    {

        if (PlayerData.Instance.inven.Items[num].Quantity > 0)
        {
            PlayerData.Instance.hp += PlayerData.Instance.inven.Items[num].HealingValue;
            PlayerData.Instance.inven.RemoveItem(PlayerData.Instance.inven.Items[num]);
            //아이템 사용 사운드를 넣어주세용
        }

       
    }


    


}
