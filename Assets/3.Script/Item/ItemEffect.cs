using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    [SerializeField]
    private ItemData[] itemData;
    private void Awake()
    {
        PlayerInput.UseItem += UseItem;
        MenuUI.UseItem += UseItem;
        Temp.UseItem += UseItem;

        itemData[0].Quantity = 15;
        itemData[1].Quantity = 7;
        itemData[2].Quantity = 5;
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
