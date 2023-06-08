using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //�����ϴ� ������ ��� �ؽ�Ʈ
    [SerializeField] private Text Effect_Text;
    [SerializeField] private MenuUI Menu;
    //���� ��� �����̸� �ֱ����� bool��
    private bool PotionDelay = false;
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

    public void RemoveItem(ItemData item)
    {
        if (PotionDelay) //���� ������ ���¶�� ����Ҽ� ������
        {
            return;
        }
        ItemData removeItem = Items.Find(x => x.ItemName == item.ItemName);
        if (removeItem != null)
        {

            if (removeItem.Quantity <= 1) //�������� 1�� ���Ͽ��� ����ϸ� ������ ����
            {
                removeItem.Quantity--;
                StartCoroutine(Text_co(removeItem));
                if (Menu.EnterMenuItem)
                {
                    if (Menu.InvenLength == 1)
                    {
                        Menu.ExitItem();
                        Menu.ItemMenuCount = 0;
                        Items.Remove(removeItem);
                        return;
                    }
                    else
                    {
                        Menu.ItemUpArrow();
                        Items.Remove(removeItem);
                        return;
                    }
                }
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
        PotionDelay = true;
        Effect_Text.gameObject.SetActive(true);
        Effect_Text.text = string.Format("{0} �� ����Ͽ����ϴ�. ���� �������� ���� {1} ��", item.ItemName, item.Quantity);
        yield return new WaitForSecondsRealtime(0.5f);
        Effect_Text.gameObject.SetActive(false);
        PotionDelay = false;
    }




}
