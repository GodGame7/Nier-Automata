using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_DashRight : State
{
    StateManager sm;
    float lastdashtime = 0;
    float dashbat = 0.5f;
    private void Awake()
    {
        sm = FindObjectOfType<StateManager>();
    }
    public override void Enter(State before)
    {
        Main_Player.Instance.isDash = true;
        Main_Player.Instance.anim_player.SetBool("DashEnd", false);
        lastdashtime = Time.time;
        Dash();
    }

    public override void Exit(State next)
    {

    }

    public override void StateFixedUpdate()
    {

    }

    public override void StateUpdate()
    {
        
    }



    private IEnumerator DashRight()
    {
        Main_Player.Instance.isDash = true;
        Main_Player.Instance.anim_player.SetTrigger("DashRight");
        while (Time.time - lastdashtime < dashbat)
        {
            transform.Translate(Vector3.right * 15f * Time.deltaTime);
            if (Time.time - lastdashtime < 0.3f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("닷지");
                    StartCoroutine(Dodge());
                    yield break;
                }
            }
            yield return null;
        }
        Main_Player.Instance.anim_player.SetBool("DashEnd", true);
        yield return new WaitForSeconds(0.4f);
        Main_Player.Instance.isDash = false;
    }

    private void Dash()
    {
            StartCoroutine(DashRight());
    }

    private IEnumerator Dodge()
    {
        Main_Player.Instance.isDodge = true;
        Main_Player.Instance.anim_player.applyRootMotion = true;
        //todo 닷지 애니메이션 실행 , 닷지 애니메이션 종료 후 닷지끝애니메이션, isDodge = false
        Main_Player.Instance.anim_player.SetBool("DodgeRight", true);
        //yield return new WaitForSeconds(1.5f);
        yield return new WaitUntil(() =>
        Main_Player.Instance.anim_player.GetCurrentAnimatorStateInfo(0).IsName("dodge3_right"));
        yield return new WaitUntil(() =>
        !(Main_Player.Instance.anim_player.GetCurrentAnimatorStateInfo(0).IsName("dodge3_right")));

        Main_Player.Instance.isDodge = false;
        Main_Player.Instance.anim_player.SetBool("DodgeRight", false);
        Main_Player.Instance.anim_player.applyRootMotion = false;
        Main_Player.Instance.isDash = false;

    }
}
