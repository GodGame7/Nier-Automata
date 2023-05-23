using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    [SerializeField] private Text Effect_Text;
    [SerializeField] Temp temp;
    

    private InventoryUI Inven_UI;
    private void Awake()
    {
        
        Temp.OnItem += UseItem;
        

    }
    //public void Aciton(int num)
    //{
    //    OnItem.Invoke(num);
    //}
    public void UseItem(int num)
    {

        if (PlayerData.instance.inven.Items[num].Quantity > 0)
        {
            PlayerData.instance.hp += PlayerData.instance.inven.Items[num].HealingValue;
            PlayerData.instance.inven.RemoveItem(PlayerData.instance.inven.Items[num]);
        }

        StartCoroutine(Text_co(num));
    }


    private IEnumerator Text_co(int num)
    {
        Effect_Text.gameObject.SetActive(true);
        Effect_Text.text = string.Format("{0} 를 사용하였습니다. 남은 아이템의 갯수 {1} 개", PlayerData.instance.inven.Items[num].ItemName, PlayerData.instance.inven.Items[num].Quantity);
        yield return new WaitForSeconds(2f);
        Effect_Text.gameObject.SetActive(false);
    }

    
}
