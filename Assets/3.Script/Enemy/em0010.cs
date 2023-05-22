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

            //�Ÿ�Ȯ��
            Distance();

            //���� ����� ���ö�, ���ݸ�Ǹ� ����
            if (current_animation.Contains("Attack") ||
                current_animation.Contains("Hit") ||
                current_animation.Contains("Die"))
            {
                yield return null;
                continue;
            }

            //Ÿ���� �ٶ󺸴� �޼���
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
                    //������ ������ŭ���� ��������
                    UpdateAttack(pattonNum);
                    break;
            }
            yield return null;
        }
    }
}
