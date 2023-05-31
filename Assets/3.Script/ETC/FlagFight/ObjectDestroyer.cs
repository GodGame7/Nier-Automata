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
        if (other.CompareTag("BulletHard") || (other.CompareTag("BulletSoft")))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out FlagEmInformation flagEmInformation))
            {
                flagEmInformation.Disappear();
            }
        }
        else if (other.CompareTag("FlagBullet"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
