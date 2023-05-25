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
        while (!isdead)
        {
            TargetLookat();
            switch (state)
            {
                case State.IDLE:
                    yield return StartCoroutine(UpdateIdle());
                    break;

                case State.WALK:
                    UpdateWalk();
                    break;

                case State.ATTACK:
                    //������ ������ŭ���� ��������
                    yield return StartCoroutine(UpdateAttack(pattonNum));
                    break;
            }
            yield return null;
        }
    }
}
