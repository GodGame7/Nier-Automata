using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flag 적의 체력, 대미지 받기, 사망, 사망 이벤트 제공
public class FlagEmInformation : MonoBehaviour
{
    [Header("Enemy 체력")]
    float maxHp = 50f;
    public float MaxHp => maxHp;

    [Header("Enemy 폭발")]
    [SerializeField] GameObject em;
    [SerializeField] GameObject explosion;

    // 확인용
    [SerializeField] public bool isDie = false;
    [SerializeField] float currentHP;
    public float CurrentHp => currentHP;
    [SerializeField] FlagFightManager fightManager;
    [SerializeField] FlagFightSpawner flagFightSpawner;

    [SerializeField] bool isBoss = false;

    private new Collider collider;

    public bool onHP = false;

    private void Start()
    {
        em.SetActive(true);
        explosion.SetActive(false);
        collider = GetComponent<Collider>();
        collider.enabled = true;
        flagFightSpawner = FindObjectOfType<FlagFightSpawner>();
        fightManager = FindObjectOfType<FlagFightManager>();
        currentHP = maxHp;
        isDie = false;
        onHP = false;
    }

    /*에너미 스폰서가 생겼으면 좋겠다.*/
    private void OnEnable()
    {
        em.SetActive(true);
        explosion.SetActive(false);
        collider = GetComponent<Collider>();
        collider.enabled = true;
        if (fightManager == null)
        {
            fightManager = FindObjectOfType<FlagFightManager>();
        }
        if (flagFightSpawner == null)
        {
            flagFightSpawner = FindObjectOfType<FlagFightSpawner>();
        }
        currentHP = maxHp;
        isDie = false;
    }

    public void OnDamage(float damage)
    {
        currentHP -= damage;
        onHP = true;
        StopCoroutine(nameof(OffDamaged_co));
        StartCoroutine(nameof(OffDamaged_co));
        if (currentHP <= 0 && !isDie)
        {
            Die();
        }
    }
    private IEnumerator OffDamaged_co()
    {
        yield return new WaitForSeconds(0.8f);
        onHP = false;
    }

    public void Disappear()
    {
        flagFightSpawner.RemainEnemies--;
        isDie = true;
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void Die()
    {
        flagFightSpawner.RemainEnemies--;
        if (isBoss)
        {
            flagFightSpawner.RemainEnemies -= 4;
        }
        isDie = true;
        em.SetActive(false);
        explosion.SetActive(true);
        collider.enabled = false;
        StartCoroutine(Co_Dying());
    }

    IEnumerator Co_Dying()
    {
        yield return new WaitForSeconds(1.0f);
        // 완전히 사라진 후 비활성화
        gameObject.SetActive(false);
    }


}
