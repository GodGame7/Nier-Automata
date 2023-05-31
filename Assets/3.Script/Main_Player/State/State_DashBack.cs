using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_DashBack : State
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
    private IEnumerator DashBack()
    {
        Main_Player.Instance.anim_player.SetTrigger("DashBack");
        Main_Player.Instance.isDash = true;
        while (Time.time - lastdashtime < dashbat)
        {
            transform.Translate(Vector3.back * 15f * Time.deltaTime);
            if (Time.time - lastdashtime < 0.3f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Debug.Log("´åÁö");
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
            StartCoroutine(DashBack());
    }


}
