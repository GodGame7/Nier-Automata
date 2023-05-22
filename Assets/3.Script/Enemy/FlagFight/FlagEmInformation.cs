using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flag ���� ü��, ����� �ޱ�, ���, ��� �̺�Ʈ ����
public class FlagEmInformation : MonoBehaviour
{
    [SerializeField] float maxHp = 50f;

    // Ȯ�ο�
    [SerializeField] public bool isDie = false;
    [SerializeField] public float currentHP;

    /*���ʹ� �������� �������� ���ڴ�.*/
    //private void OnEnable()
    //{
    //    currentHP = maxHp;
    //    isDie = false;
    //}


    private void Start()
    {
        currentHP = maxHp;
        isDie = false;
    }
    
    public void OnDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDie = true;
        Debug.Log("����");
    }
}
