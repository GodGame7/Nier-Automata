using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Text Effect_Text;
    public List<ItemData> Items = new List<ItemData>();
    public void AddItem(ItemData item) //������ ȹ���
    {
        int itemIndex = Items.FindIndex(x => x.Quantity == item.Quantity);

        if (itemIndex < 0) //�������� ���ٸ� ������ ����
        {
            Items.Add(item);
        }
        else //�������� �ִٸ� ������ ���� �߰�
        {
            Items[itemIndex].Quantity++;
        }
    }
    //public void RemoveItem(ItemData item)
    //{
    //    int itemIndex = Items.FindIndex(x => x.Quantity == item.Quantity);
    //    if (itemIndex >= 0)
    //    {
    //        ItemData removeItem = Items[itemIndex];
    //        if (Items[itemIndex].Quantity <= 1) //�������� 1�� ���Ͽ��� ����ϸ� ������ ����
    //        {

    //            StartCoroutine(Text_co(removeItem));
    //            Items.Remove(item);
    //        }
    //        else //�������� 2�� �̻��̶�� ������ ���� -1
    //        {
    //            Items[itemIndex].Quantity--;
    //            StartCoroutine(Text_co(removeItem));
    //        }
    //    }
    //}
    public void RemoveItem(ItemData item)
    {
        ItemData removeItem = Items.Find(x => x.ItemName == item.ItemName);
        if (removeItem !=null)
        {
            
            if (removeItem.Quantity <= 1) //�������� 1�� ���Ͽ��� ����ϸ� ������ ����
            {
                removeItem.Quantity--;
                StartCoroutine(Text_co(removeItem));
                
                Items.Remove(removeItem);
            }
            else //�������� 2�� �̻��̶�� ������ ���� -1
            {
                removeItem.Quantity--;
                StartCoroutine(Text_co(removeItem));
            }
        }
    }
    private IEnumerator Text_co(ItemData item)
    {

        Effect_Text.gameObject.SetActive(true);
        Effect_Text.text = string.Format("{0} �� ����Ͽ����ϴ�. ���� �������� ���� {1} ��", item.ItemName, item.Quantity);
        yield return new WaitForSeconds(0.5f);
        Effect_Text.gameObject.SetActive(false);

    }




}
