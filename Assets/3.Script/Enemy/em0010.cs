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
            //거리확인
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
                    //패턴의 갯수만큼에서 랜덤공격
                    yield return StartCoroutine(UpdateAttack2(pattonNum));
                    break;
            }
            yield return null;
        }
    }
}
