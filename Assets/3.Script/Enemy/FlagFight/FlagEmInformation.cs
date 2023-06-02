using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flag ���� ü��, ����� �ޱ�, ���, ��� �̺�Ʈ ����
public class FlagEmInformation : MonoBehaviour
{
    [Header("Enemy ü��")]
    [SerializeField] float maxHp = 50f;

    [Header("Enemy ����")]
    [SerializeField] GameObject em;
    [SerializeField] GameObject explosion;

    // Ȯ�ο�
    [SerializeField] public bool isDie = false;
    [SerializeField] public float currentHP;
    [SerializeField] FlagFightManager fightManager;
    [SerializeField] FlagFightSpawner flagFightSpawner;

    [SerializeField] bool isBoss = false;

    private new Collider collider;


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
    }

    /*���ʹ� �������� �������� ���ڴ�.*/
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
        if (currentHP <= 0 && !isDie)
        {
            Die();
        }
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
        if(isBoss)
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
        // ������ ����� �� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }


}
