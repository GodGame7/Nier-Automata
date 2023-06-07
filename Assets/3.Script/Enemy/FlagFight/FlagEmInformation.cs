using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Flag 적의 체력, 대미지 받기, 사망, 사망 이벤트 제공
public class FlagEmInformation : MonoBehaviour
{
    [Header("Enemy 체력")]
    [SerializeField] float maxHp = 50f;
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
    [SerializeField]
    private GameObject DamagePrefab;

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
        DamageEffect(damage, transform.position);

        if (currentHP <= 0 && !isDie)
        {
            Die();
        }
    }
    private void DamageEffect(float damage, Vector3 position)
    {
        // 데미지 화면에 띄우기
        GameObject Damage = Instantiate(DamagePrefab);
        Damage.transform.SetParent(GameObject.FindWithTag("Canvas").transform, false);
        Text damageText = Damage.GetComponent<Text>();

        // 수치 설정
        damageText.text = damage.ToString();
        // 위치 설정
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(position);
        Damage.transform.position = screenPoint;

        //// 데미지 사라짐
        //for (float alpha = 1f; alpha >= 0f; alpha -= 1.5f * Time.deltaTime)
        //{
        //    Color newColor = damageText.color;
        //    newColor.a = alpha;
        //    damageText.color = newColor;
        //    yield return null;
        //    Damage.transform.position += 0.5f * Vector3.up;
        //}
        //Destroy(Damage);
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
