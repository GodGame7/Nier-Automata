using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em0010 : Enemy
{

    private void Start()
    {
        StartCoroutine(CheckState());
    }

    IEnumerator CheckState()
    {
        while (!isdead)
        {
            //�Ÿ�Ȯ��
            Distance();

            TargetLookat();

            switch (state)
            {
                case State.IDLE:
                    yield return StartCoroutine(UpdateIdle2());
                    break;

                case State.WALK:
                    UpdateWalk();
                    break;

                case State.ATTACK:
                    //������ ������ŭ���� ��������
                    yield return StartCoroutine(UpdateAttack2(pattonNum));
                    break;
            }
            yield return null;
        }
    }
}
