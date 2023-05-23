using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FlagFightUIManager : MonoBehaviour
{
    [Header("Flag Fight UI들을 넣어주세요")]
    [SerializeField] GameObject background01;
    [SerializeField] GameObject background02;
    [SerializeField] GameObject subTitle;

    [Space(0.5f)]
    [Header("Managers")]
    [SerializeField] FlagFightManager flagFightManager;

    private void Awake()
    {
        //
    }




}