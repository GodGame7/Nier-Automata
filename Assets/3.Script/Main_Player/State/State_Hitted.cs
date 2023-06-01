using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Hitted : State
{
    public override void Enter(State before)
    {
        Main_Player.Instance.isHitted = true;
        StartCoroutine(Hitted_co());
    }

    public override void Exit(State next)
    {
        Main_Player.Instance.isHitted = false;
    }

    public override void StateFixedUpdate()
    {

    }

    public override void StateUpdate()
    {

    }

    float HitNum = 0;
    private IEnumerator Hitted_co()
    {
        Main_Player.Instance.anim_player.SetFloat("HitNum", (HitNum % 2));
        Main_Player.Instance.anim_player.SetBool("Hitted", true);
        Main_Player.Instance.anim_player.SetTrigger("HittedTrigger");
        HitNum++;
        Debug.Log("����");
        yield return new WaitForSeconds(0.5f);
        Main_Player.Instance.isHitted = false;
        //todo ��Ƽ�� ������ ��, ����Ʈ���μ���
    }
}
