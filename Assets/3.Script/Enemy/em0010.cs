using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class em0010 : Enemy
{
    public AudioClip Walk;
    public AudioClip JumpStart;
    public AudioClip JumpEnd;
    public AudioClip LeftPunch;
    public AudioClip RightPunch;
    public AudioClip doubleAttack;

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
                    //패턴의 갯수만큼에서 랜덤공격
                    yield return StartCoroutine(OnAttack(pattonNum));
                    break;
            }
            yield return null;
        }
    }

    void WalkSound()
    {
        audio.PlayOneShot(Walk);
    }

    void DoubleAttackSound()
    {
        audio.PlayOneShot(doubleAttack);
    }
    void RightPunchSound()
    {
        audio.PlayOneShot(RightPunch);
    }

    void LeftPunchSound()
    {
        audio.PlayOneShot(LeftPunch);
    }

    void JumpStartSound()
    {
        audio.PlayOneShot(JumpStart);
    }

    void JumpEndSound()
    {
        audio.PlayOneShot(JumpEnd);
    }


}
