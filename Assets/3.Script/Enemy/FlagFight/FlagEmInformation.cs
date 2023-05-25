using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Flag 적의 체력, 대미지 받기, 사망, 사망 이벤트 제공
public class FlagEmInformation : MonoBehaviour
{
    [Header("Enemy 체력")]
    [SerializeField] float maxHp = 50f;

    [Header("Enemy 폭발")]
    [SerializeField] GameObject explosion;

    // 확인용
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

    /*에너미 스폰서가 생겼으면 좋겠다.*/
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

        // 부모 오브젝트의 CanvasGroup 가져오기
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // 자식 오브젝트들의 CanvasGroup 배열 가져오기
        CanvasGroup[] childCanvasGroups = gameObject.GetComponentsInChildren<CanvasGroup>();

        // 부모 오브젝트와 자식 오브젝트들에 CanvasGroup 컴포넌트가 없다면 추가하기
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

            // 부모 오브젝트의 CanvasGroup 업데이트
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            // 자식 오브젝트들의 CanvasGroup 업데이트
            foreach (CanvasGroup childCanvasGroup in childCanvasGroups)
            {
                childCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            }

            yield return null;
        }

        // 마지막에 알파 값을 1로 설정
        foreach (CanvasGroup childCanvasGroup in childCanvasGroups)
        {
            childCanvasGroup.alpha = 1f;
        }

        // 부모 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
