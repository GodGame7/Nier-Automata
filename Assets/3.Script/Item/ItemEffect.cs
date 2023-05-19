using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEffect : MonoBehaviour
{
    [SerializeField] ItemData[] ItemData;
    [SerializeField] private Text Effect_Text;
    public void UseItem(int num)
    {

        if (ItemData[num].CurrntItem > 0)
        {
            if (num < 3) // 포션을 사용했을때 
            {
                //Player.curHp += ItemData[num].HelingValue;
            }
        }
        
        StartCoroutine(Text_co(num));
    }

    private IEnumerator Text_co(int num)
    {
        Effect_Text.gameObject.SetActive(true);
        Effect_Text.text = string.Format("{0} 를 사용하였습니다. 남은 아이템의 갯수 {1} 개", ItemData[num].ItemName, ItemData[num].CurrntItem);
        yield return new WaitForSeconds(2f);
        Effect_Text.gameObject.SetActive(false);
    }
}
