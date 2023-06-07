using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FlagFightManager : MonoBehaviour
{
    // Unity 이벤트 정의
    public UnityEvent phase1;

    [SerializeField] FlagFightSpawner flagFightSpawner;

    private void Awake()
    {
        flagFightSpawner = FindObjectOfType<FlagFightSpawner>();

        flagFightSpawner.Phase18_01_EMDie.AddListener(NextPhase);
    }

    private void Start()
    {
        phase1.Invoke();
    }

    private void NextPhase()
    {
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(4.0f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
