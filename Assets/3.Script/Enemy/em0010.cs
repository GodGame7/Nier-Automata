using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em0010 : Enemy
{
    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {
        while (!enemyHp.isdead)
        {
            TargetLookat();
            switch (state)
            {
                case State.IDLE:
                    yield return StartCoroutine(OnIdle());
                    break;

                case State.WALK:
                    Onwalk();
                    break;

                case State.ATTACK:
                    //������ ������ŭ���� ��������
                    yield return StartCoroutine(OnAttack(pattonNum));
                    break;
            }
            yield return null;
        }
    }
}
