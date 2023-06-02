using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogControl : MonoBehaviour
{
    [Header("�ӵ�")]
    [SerializeField] float fadeOutDuration = 2.0f;

    [Space(0.5f)]
    [Header("Ȯ�ο�")]
    [SerializeField] FlagFightSubTitleManager flagFightSubTitleManager;
    [SerializeField] new ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        flagFightSubTitleManager = FindObjectOfType<FlagFightSubTitleManager>();
        flagFightSubTitleManager.phase1_13.AddListener(phase01_13);
    }

    void phase01_13()
    {
        StartCoroutine(Co_fadeOut());
    }

    private IEnumerator Co_fadeOut()
    {
        // ��ƼŬ �ý����� ���� ����� �����ɴϴ�.
        ParticleSystem.MainModule mainModule = particleSystem.main;

        // ���̵� �ƿ� ȿ���� �����ϱ� ���� ���� ���� ���� Ÿ�̸Ӹ� �ʱ�ȭ�մϴ�.
        float startAlpha = mainModule.startColor.color.a;
        float fadeOutTimer = 0f;

        while (fadeOutTimer < fadeOutDuration)
        {
            // ���̵� �ƿ� Ÿ�̸Ӹ� ������Ʈ�մϴ�.
            fadeOutTimer += Time.deltaTime;

            // ���̵� �ƿ� ȿ���� �����ϱ� ���� ���� ���� �����մϴ�.
            float alpha = Mathf.Lerp(startAlpha, 0f, fadeOutTimer / fadeOutDuration);
            mainModule.startColor = new Color(mainModule.startColor.color.r, mainModule.startColor.color.g, mainModule.startColor.color.b, alpha);

            yield return null;
        }

        // ���̵� �ƿ��� �Ϸ�Ǹ� ��ƼŬ �ý����� �����մϴ�.
        particleSystem.Stop();
    }
}