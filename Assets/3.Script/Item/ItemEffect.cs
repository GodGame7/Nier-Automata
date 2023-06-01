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

        if (PlayerData.instance.inven.Items[num].Quantity > 0)
        {
            PlayerData.instance.hp += PlayerData.instance.inven.Items[num].HealingValue;
            PlayerData.instance.inven.RemoveItem(PlayerData.instance.inven.Items[num]);
            //아이템 사용 사운드를 넣어주세용
        }

       
    }


    


}
