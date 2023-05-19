using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagFightManager : MonoBehaviour
{
    // Unity 이벤트 정의
    public UnityEvent event1;

    private void Awake()
    {
        // 이벤트 추가
        event1.AddListener(OnUnityEvent);
    }

    private void Start()
    {
        event1.Invoke();
    }

    void OnUnityEvent()
    {
        Debug.Log("event1");
    }
}
