using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPPositionSetter : MonoBehaviour
{
    public GameObject Target;
    private RectTransform UITrans;
    public FlagEmInformation monster;

    private Vector3 distance = 80 * Vector3.up + -5 * Vector3.right;

    private void OnEnable()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void SetUp(GameObject target)
    {
        this.Target = target;
        TryGetComponent(out UITrans);
        Target.TryGetComponent(out monster);
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(monster.onHP);
        }
        if (!Target.activeSelf)
        {
            Destroy(gameObject);
            return;
        }

        // ���� ī�޶� ��ġ���� ���ʹ� ��ġ ã��
        Vector3 ScreenPosition = Camera.main.WorldToScreenPoint(Target.transform.position);

        UITrans.position = ScreenPosition + distance + Target.transform.position.x * 104 * Vector3.right + Target.transform.position.z * 100 * Vector3.up;
    }
}
