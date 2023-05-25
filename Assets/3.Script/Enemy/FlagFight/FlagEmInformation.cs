using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flag ���� ü��, ����� �ޱ�, ���, ��� �̺�Ʈ ����
public class FlagEmInformation : MonoBehaviour
{
    [Header("Enemy ü��")]
    [SerializeField] float maxHp = 50f;

    [Header("Enemy ����")]
    [SerializeField] GameObject explosion;

    // Ȯ�ο�
    [SerializeField] public bool isDie = false;
    [SerializeField] public float currentHP;
    [SerializeField] FlagFightManager fightManager;
    [SerializeField] FlagFightSpawner flagFightSpawner;

    private new Collider collider;


    private void Start()
    {
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
        explosion.SetActive(false);
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
        gameObject.SetActive(false);
    }

    public void Die()
    {
        flagFightSpawner.RemainEnemies--;
        isDie = true;
        explosion.SetActive(true);
        collider.enabled = false;
        StartCoroutine(Co_Dying());
    }

    IEnumerator Co_Dying()
    {

        // �θ� ������Ʈ�� CanvasGroup ��������
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // �ڽ� ������Ʈ���� CanvasGroup �迭 ��������
        CanvasGroup[] childCanvasGroups = gameObject.GetComponentsInChildren<CanvasGroup>();

        // �θ� ������Ʈ�� �ڽ� ������Ʈ�鿡 CanvasGroup ������Ʈ�� ���ٸ� �߰��ϱ�
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        foreach (Transform child in transform)
        {
            if (child.GetComponent<CanvasGroup>() == null)
            {
                child.gameObject.AddComponent<CanvasGroup>();
            }
        }

        float counter = 0f;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;

        while (counter < 1.00f)
        {
            counter += Time.deltaTime;
            float t = Mathf.Clamp01(counter / 1.00f);

            // �θ� ������Ʈ�� CanvasGroup ������Ʈ
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            // �ڽ� ������Ʈ���� CanvasGroup ������Ʈ
            foreach (CanvasGroup childCanvasGroup in childCanvasGroups)
            {
                childCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            }

            yield return null;
        }

        // �������� ���� ���� 1�� ����
        foreach (CanvasGroup childCanvasGroup in childCanvasGroups)
        {
            childCanvasGroup.alpha = 1f;
        }

        // �θ� ������Ʈ ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
