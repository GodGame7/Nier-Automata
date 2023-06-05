using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    [SerializeField]
    private Slider sliderHP;
    [SerializeField]
    private Enemy enemy;

    public void SetUp(Enemy enemy)
    {
        this.enemy = enemy;
        TryGetComponent(out sliderHP);
    }

    void Update()
    {
        if (!enemy.Equals(null))
        {
            sliderHP.value = enemy.enemyHp.currentHp / (float)enemy.enemyHp.maxHp;
        }
    }
}
