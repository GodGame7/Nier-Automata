using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class em0000 : Enemy
{
    //Ÿ�̸�
    Stopwatch sw = new Stopwatch();
    enum ADDState
    {
        DASH = 3
    }

    private void Update()
    {
        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
        string current_animation = animatorinfo[0].clip.name;

        //�Ÿ� Ȯ��
        Distance();

        //���� ����� ���ö�, ���ݸ�Ǹ� ����
        if (current_animation.Contains("Attack") ||
            current_animation.Contains("Hit") ||
            current_animation.Contains("Die"))
        {
            return;
        }

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
    }

    //IEnumerator CheckState()
    //{
    //    while (!isdead)
    //    {
    //        AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
    //        animatorinfo = this.anim.GetCurrentAnimatorClipInfo(0);
    //        string current_animation = animatorinfo[0].clip.name;

    //        //�Ÿ� Ȯ��
    //        Distance();

    //        //���� ����� ���ö�, ���ݸ�Ǹ� ����
    //        if (current_animation.Contains("Attack") ||
    //            current_animation.Contains("Hit") ||
    //            current_animation.Contains("Die"))
    //        {
    //            yield return null;
    //            continue;
    //        }

    //        //Ÿ���� �ٶ󺸴� �޼���
    //        TargetLookat();

    //        switch (state)
    //        {
    //            case State.IDLE:
    //                UpdateIdle();
    //                break;

    //            case State.WALK:
    //                UpdateWalk();
    //                break;

    //            case State.ATTACK:
    //                UpdateAttack(pattonNum);
    //                break;

    //            case (State)ADDState.DASH:
    //                UpdateDash();
    //                break;
    //        }
    //        yield return null;
    //    }

    //}


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
        int random = Random.Range(1, PattonNum + 1);
        sw.Start();
        switch (random)
        {
            case 1:
                WindmillAttack();
                break;
            case 2:
                PunchAttack();
                break;
        }
    }

    void WindmillAttack()
    {
        UnityEngine.Debug.Log(sw.ElapsedMilliseconds);

        if (!anim.GetBool("Attack2"))
        {
            anim.SetBool("Attack2", true);
        }
        else if (anim.GetBool("Attack2") && (sw.ElapsedMilliseconds > 4000.0f))
        {
            UnityEngine.Debug.Log("�ð� �� �ȵ����� ?");
            anim.SetBool("Attack2", false);
            state = State.IDLE;
            sw.Reset();
            return;
        }

    }



    void PunchAttack()
    {
        anim.SetTrigger("Attack1");
        state = State.IDLE;
        sw.Reset();
        return;
    }

    void UpdateDash()
    {
        anim.SetTrigger("Dash");
        state = State.WALK;
    }

}
