using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_DashBack : State
{
    StateManager sm;
    float lastdashtime = 0;
    float dashbat = 0.5f;
    float dashtime = 5f;
    private void Awake()
    {
        sm = FindObjectOfType<StateManager>();
    }
    public override void Enter(State before)
    {
        dashtime = 5f;
        Main_Player.Instance.isDash = true;
        Main_Player.Instance.anim_player.SetBool("DashEnd", false);
        lastdashtime = Time.time;
        Dash();
    }
    public override void Exit(State next)
    {
        Main_Player.Instance.meshBake.OffTrail();
        Main_Player.Instance.rb.velocity = Vector3.zero;

    }
    public override void StateFixedUpdate()
    {

    }
    public override void StateUpdate()
    {
        if (dashtime < 0.5f)
        {
            transform.Translate(Vector3.back * 10f * Time.deltaTime);
            dashtime += Time.deltaTime;
        }
    }
    private IEnumerator DashBack()
    {
        Main_Player.Instance.anim_player.SetTrigger("DashBack");
        Main_Player.Instance.isDash = true;
        AudioManager.Instance.PlaySfx(Define.SFX.Dash2);

        Main_Player.Instance.meshBake.OnTrail();
        while (Time.time - lastdashtime < dashbat)
        {
            transform.Translate(Vector3.back * 15f * Time.deltaTime);
            if (Time.time - lastdashtime < 0.3f)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(Dodge());
                    yield break;
                }
            }
            yield return null;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Main_Player.Instance.isDash = false;
            yield break;
        }
        Main_Player.Instance.anim_player.SetBool("DashEnd", true);
        yield return new WaitForSeconds(0.4f);
        Main_Player.Instance.isDash = false;
    }


    private void Dash()
    {
            StartCoroutine(DashBack());
    }


    private IEnumerator Dodge()
    {
        Main_Player.Instance.isDodge = true;
        Main_Player.Instance.anim_player.SetBool("DodgeBack", true);
        dashtime = 0f;
        yield return new WaitUntil(() =>
        Main_Player.Instance.anim_player.GetCurrentAnimatorStateInfo(0).IsName("dodge_back"));
        yield return new WaitUntil(() =>
        (Main_Player.Instance.anim_player.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.58f));
        yield return CancleListener();
        Main_Player.Instance.isDodge = false;
        Main_Player.Instance.anim_player.SetBool("DodgeBack", false);
        Main_Player.Instance.isDash = false;
    }
    private IEnumerator CancleListener()
    {
        yield return new WaitForEndOfFrame();
        float count = 0f;
        while (count < 0.8f)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetMouseButton(0))
            {
                break;
            }
            count += Time.deltaTime;
            yield return null;
        }
    }
}
