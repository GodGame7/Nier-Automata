using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogControl : MonoBehaviour
{
    public FlagFightSubTitleManager flagFightSubTitleManager;

    private void Awake()
    {
        flagFightSubTitleManager = FindObjectOfType<FlagFightSubTitleManager>();
        flagFightSubTitleManager.phase1_13.AddListener(phase01_13);
    }

    void phase01_13()
    {
        gameObject.SetActive(false);
    }
}