using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class em0000 : MonoBehaviour
{
    enum State
    {
        IDLE,
        WALK,
        JUMP,
        DASH,
        ATTACK,
        DIE,
    }

    float movespeed = 1f;
    bool isdead = false;

    //목적지
    public Transform target;

    //요원
    NavMeshAgent agent;
    Animator anim;

    [SerializeField]State state;

    private void Start()
    {

        state = State.IDLE;

        TryGetComponent(out anim);
        TryGetComponent(out agent);

        target = GameObject.Find("Player").transform;
        //요원에게 목적지를 알려준다.

        StartCoroutine(CheckState());
    }


    void Walk()
    {
        anim.SetBool("Walk", true);
        //남은 거리가 2미터라면 공격한다.

        //타겟 방향으로 이동하다가
        //agent.speed = 0.1f;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        Debug.Log(distance);


        if (distance <= 0.5f)
        {
            state = State.ATTACK;
        }
        else if(distance > 2f)
        {
            state = State.DASH;
        }
    }


    void Idle()
    {
        state = State.WALK;
    }

    void Attack()
    {
        anim.SetTrigger("Attack2");
        
        if (true)
        {

        }
    }

    void UpdateDash()
    {
        anim.SetTrigger("Dash");
    }

    IEnumerator CheckState()
    {
        while (!isdead)
        {
            yield return new WaitForSeconds(0.2f);

            agent.destination = target.transform.position;

            switch (state)
            {
                case State.IDLE:
                    Idle();
                    break;

                case State.WALK:
                    Walk();
                    break;

                case State.JUMP:
                    break;

                case State.DASH:
                    UpdateDash();
                    break;

                case State.ATTACK:
                    Attack();
                    break;

                case State.DIE:
                    break;

                default:
                    break;
            }
        }

    }
}
