using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDamage : MonoBehaviour
{
    [SerializeField] Text damageText;

    private void OnEnable()
    {
        damageText = GetComponent<Text>();
        StartCoroutine(DamageLifeSecond());
    }

    IEnumerator DamageLifeSecond()
    {
        // 데미지 사라짐
        for (float alpha = 1f; alpha >= 0f; alpha -= 1.5f * Time.deltaTime)
        {
            Color newColor = damageText.color;
            newColor.a = alpha;
            damageText.color = newColor;
            yield return null;
            transform.position += 0.5f * Vector3.up;
        }
        Destroy(gameObject);
    }
}
