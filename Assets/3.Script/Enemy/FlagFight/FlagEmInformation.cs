using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flag 적의 체력, 대미지 받기, 사망, 사망 이벤트 제공
public class FlagEmInformation : MonoBehaviour
{
    [SerializeField] float maxHp = 50f;

    // 확인용
    [SerializeField] public bool isDie = false;
    [SerializeField] public float currentHP;

    /*에너미 스폰서가 생겼으면 좋겠다.*/
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
        Debug.Log("죽음");
    }
}
