using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    public GameObject target_obj;
    [SerializeField]private RectTransform UITrans;
    private Camera mainCamera;

    //////private Vector3 distance = new Vector3(-200, 150, 0);

    private void Awake()
    {
        TryGetComponent(out UITrans);
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        //Vector3 ScreenPosition = mainCamera.WorldToScreenPoint(target_obj.transform.position);

        UITrans.position = target_obj.transform.position;// + new Vector3(0, 150f, 0);// + distance;
        //UITrans.eulerAngles = Quaternion;
    }

}
