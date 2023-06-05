using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    [SerializeField]
    private Slider sliderHP;
    [SerializeField]
    private FlagEmInformation enemy;

    public void SetUp(FlagEmInformation enemy)
    {
        this.enemy = enemy;
        TryGetComponent(out sliderHP);
    }

    void Update()
    {
        if (!enemy.Equals(null))
        {
            sliderHP.value = enemy.CurrentHp / (float)enemy.MaxHp;
        }
    }
}
