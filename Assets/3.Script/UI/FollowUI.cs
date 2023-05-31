using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour
{
    public GameObject target_obj;
    private RectTransform UITrans;

    private Vector3 distance = new Vector3(-200, 150, 0);

    private void Awake()
    {
        TryGetComponent(out UITrans);
    }

    private void LateUpdate()
    {
        if (!target_obj.activeSelf)
        {
            gameObject.SetActive(false);
            return;
        }
        Vector3 ScreenPosition = Camera.main.WorldToScreenPoint(target_obj.transform.position);

        UITrans.position = ScreenPosition + distance;
    }
}
