using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // ��ü�� �����ؼ� ������ ������ �����ϰų� ��Ȱ��ȭ
    [Header("��ü�� ������ �ݶ��̴�")]
    [SerializeField] public Collider objectDestroyer;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("BulletHard") || (other.tag.Equals("BulletSoft")))
        {
            Destroy(other.gameObject);
        }
        else if (other.tag.Equals("Enemy"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
