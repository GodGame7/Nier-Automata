using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlagFightManager : MonoBehaviour
{
    // Unity �̺�Ʈ ����
    public UnityEvent event1;

    private void Awake()
    {
        // �̺�Ʈ �߰�
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
