using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class em0000 : Enemy
{
    private void Start()
    {
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
                    yield return StartCoroutine(UpdateAttack(pattonNum));
                    break;

                case State.DASH:
                    yield return StartCoroutine(UpdateDash());
                    break;
            }
            yield return null;
        }

    }

    protected override IEnumerator UpdateAttack(int PattonNum)
    {
        state = State.ATTACK;

        int random = Random.Range(1, PattonNum + 1);

        switch (random)
        {
            case 1:
                yield return StartCoroutine(WindmillAttack2());
                break;

            case 2:
                yield return StartCoroutine(PunchAttact2());
                break;
        }
        state = State.IDLE;
    }

    IEnumerator WindmillAttack2()
    {
        anim.SetBool("Attack2", true);
        yield return new WaitForSeconds(4f);
        anim.SetBool("Attack2", false);

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));
    }

    IEnumerator PunchAttact2()
    {
        anim.SetTrigger("Attack1");

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("em0000_Idle"));
    }

}
