using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FlagFightUIManager : MonoBehaviour
{
    [Header("Flag Fight UI들을 넣어주세요")]
    [SerializeField] GameObject background01;
    [SerializeField] GameObject background02;
    [SerializeField] GameObject subTitle;

    [Space(0.5f)]
    [Header("Managers")]
    [SerializeField] FlagFightManager flagFightManager;
    [SerializeField] FlagFightSubTitleManager flagFightSubTitleManager;

    private void Awake()
    {
        flagFightSubTitleManager.phase1_01.AddListener(phase1_01);
        flagFightSubTitleManager.phase1_09.AddListener(phase1_09);
        flagFightSubTitleManager.phase1_12.AddListener(phase1_12);
        flagFightSubTitleManager.phase1_15.AddListener(phase1_15);
    }


    private void phase1_01()
    {
        StartCoroutine(FadeOut(background02, 3.0f));
    }

    private void phase1_09()
    {
        StartCoroutine(FadeOut(background01, 3.0f));
    }

    private void phase1_12()
    {
        StartCoroutine(FadeIn(background01, 3.0f));
    }

    private void phase1_15()
    {
        StartCoroutine(FadeOut(background01, 2.0f));
    }

    private IEnumerator FadeOut(GameObject obj, float duration)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        float counter = 0f;
        float startAlpha = canvasGroup.alpha;
        float targetAlpha = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float t = Mathf.Clamp01(counter / duration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }

        // 완전히 사라진 후 비활성화
        obj.SetActive(false);
    }

    private IEnumerator FadeIn(GameObject obj, float duration)
    {
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        obj.SetActive(true); // 오브젝트를 활성화.

        float counter = 0f;
        float startAlpha = 0f;
        float targetAlpha = 1f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float t = Mathf.Clamp01(counter / duration);
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            yield return null;
        }
    }



}