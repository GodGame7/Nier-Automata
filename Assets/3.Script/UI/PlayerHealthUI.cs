using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    //[SerializeField] Text Level_Txt;
    

    [SerializeField] private Slider Hp_Slider;

    private void Update()
    {
        
        Hp_Slider.value = PlayerData.Instance._hp / PlayerData.Instance.maxHp;
        //Level_Txt.text = string.Format("Lv : {0}",PlayerData.Instance.Level); << ������ ��� ���� 1�� �������ѳ���. ����Ҷ� Level_txt�� �ּ�Ǯ��
    }


    //Invoke �����Լ� ���� ����ϸ�� 
    public void AcitveFalse()
    {
        gameObject.SetActive(false);
    }
    public void AcitveTrue()
    {
        gameObject.SetActive(true);
    }
}
