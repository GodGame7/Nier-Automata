using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    // 물체를 감지해서 밖으로 나가면 삭제하거나 비활성화
    [Header("물체를 감지할 콜라이더")]
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
