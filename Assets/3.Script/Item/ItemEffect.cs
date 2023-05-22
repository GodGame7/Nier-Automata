using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    [SerializeField] ItemData ItemData;
    [SerializeField] private Text Effect_Text;
  
    public void UseItem(int num)
    {

        if (ItemData.Quantity > 0)
        {
            //Player.curHp += player.instacne.inventory[num].HelingValue;
            //Player.instance.inventory.RemoveItem(ListNum);
        }

        StartCoroutine(Text_co());
    }


    private IEnumerator Text_co()
    {
        Effect_Text.gameObject.SetActive(true);
        Effect_Text.text = string.Format("{0} 를 사용하였습니다. 남은 아이템의 갯수 {1} 개", ItemData.ItemName, ItemData.Quantity);
        yield return new WaitForSeconds(2f);
        Effect_Text.gameObject.SetActive(false);
    }

    
}
