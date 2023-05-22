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
            AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
            string current_animation = animatorinfo[0].clip.name;

            //거리확인
            Distance();

            //공격 모션이 나올땐, 공격모션만 실행
            if (current_animation.Contains("Attack") ||
                current_animation.Contains("Hit") ||
                current_animation.Contains("Die"))
            {
                yield return null;
                continue;
            }

            //타겟을 바라보는 메서드
            TargetLookat();

            switch (state)
            {
                case State.IDLE:
                    UpdateIdle();               
                    break;

                case State.WALK:
                    UpdateWalk();
                    break;

                case State.ATTACK:
                    //패턴의 갯수만큼에서 랜덤공격
                    UpdateAttack(pattonNum);
                    break;
            }
            yield return null;
        }
    }
}
