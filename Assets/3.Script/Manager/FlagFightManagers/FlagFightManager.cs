using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagFightManager : MonoBehaviour
{
    // Unity 이벤트 정의
    public UnityEvent phase1;

    private void Awake()
    {
    }

    private void Start()
    {
        phase1.Invoke();
    }













}
