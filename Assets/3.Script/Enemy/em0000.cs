using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class em0000 : Enemy
{
    Stopwatch sw = new Stopwatch();

    enum ADDState
    {
        DASH = 3
    }

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

            //거리 확인
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
                    UpdateAttack(pattonNum);
                    break;

                case (State)ADDState.DASH:
                    UpdateDash();
                    break;
            }
            yield return null;
        }

    }


    protected override void UpdateWalk()
    {    
        base.UpdateWalk();

        if (distance >= 6f)
        {
            state = (State)ADDState.DASH;
            return;
        }
    }

    protected override void UpdateAttack(int PattonNum)
    {
        sw.Start();
        int random = Random.Range(1, PattonNum + 1);
        UnityEngine.Debug.Log("왔냐 ? 공격선택");

        switch (random)
        {
            case 1:
                UnityEngine.Debug.Log("윈드밀~");
                WindmillAttack();
                break;
            case 2:
                UnityEngine.Debug.Log("공격!");
                PunchAttack();
                break;
        }
    }

    void WindmillAttack()
    {
        if (!anim.GetBool("Attack2"))
        {
            anim.SetBool("Attack2", true);
        }
        else if (anim.GetBool("Attack2") && (distance <= 2f || sw.ElapsedMilliseconds > 4000.0f))
        {
            anim.SetBool("Attack2", false);
            state = State.IDLE;
            sw.Reset();
            return;
        }

    }

    void PunchAttack()
    {
        if (!anim.GetBool("Attack2"))
        {
            anim.SetTrigger("Attack1");
            state = State.IDLE;
            sw.Reset();
            return;
        }
    }

    void UpdateDash()
    {
        anim.SetTrigger("Dash");
        state = State.WALK;
    }

}
