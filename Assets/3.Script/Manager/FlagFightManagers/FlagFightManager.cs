using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagFightManager : MonoBehaviour
{
    // Unity �̺�Ʈ ����
    public UnityEvent phase1;

    private void Awake()
    {
    }

    private void Start()
    {
        phase1.Invoke();
    }













}
