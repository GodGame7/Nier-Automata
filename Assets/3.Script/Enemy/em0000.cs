using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class em0000 : Enemy
{

    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Walk;
    public AudioClip Dash;

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
                    yield return StartCoroutine(OnAttack(pattonNum));
                    break;

                case State.DASH:
                    yield return StartCoroutine(OnDash());
                    break;
            }

            yield return null;
        }

    }

    protected override IEnumerator OnAttack(int PattonNum)
    {
        state = State.ATTACK;

        int random = Random.Range(1, PattonNum + 1);

        switch (random)
        {
            case 1:
                yield return StartCoroutine(WindmillAttack());
                break;

            case 2:
                yield return StartCoroutine(PunchAttact());
                break;
        }
        state = State.IDLE;

    }

    IEnumerator WindmillAttack()
    {
        anim.SetBool("Attack2", true);



        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
        {
            TargetLookat();
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        anim.SetBool("Attack2", false);

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));
    }


    IEnumerator PunchAttact()
    {

        TargetLookat();
        anim.SetTrigger("Attack1");


        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));
    }

    void PlayAttack1()
    {
        audio.PlayOneShot(Attack1);
    }

    void PlayAttack2()
    {
        audio.PlayOneShot(Attack2);
    }

    void PlayWalk()
    {
        audio.PlayOneShot(Walk);
    }

    void PlayDash()
    {
        audio.PlayOneShot(Dash);
    }

}
