using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    StateManager sm;
    Coroutine co_dodge;
    Coroutine co_atk;
    [Header("대쉬 입력 대기 시간")]
    public float dashCount;
    private void Start()
    {
        sm = FindObjectOfType<StateManager>();
        co_dodge = StartCoroutine(DodgeListener());
        co_atk = StartCoroutine(AttackListener());
    }
    Vector3 inputVec;
    private void Update()
    {
        if (isCanMove())
        {
            if (sm.currentState != sm.idle)
            {
                sm.ChangeState(sm.idle);
            }
        }
    }

    public bool isCanMove()
    {
        if (!Main_Player.Instance.isAtk && !Main_Player.Instance.isDash &&!Main_Player.Instance.isHitted)
        {
            return true;
        }
        else return false;
    }


    #region 공격 리스너
    private IEnumerator AttackListener()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(sm.currentState != sm.atk)
                sm.ChangeState(sm.atk);
            }
            yield return null;
        }
    }
    #endregion
    #region Dash_Dodge 관련
    private IEnumerator DodgeListener()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                yield return DoubleWClick();
            }
            else if(Input.GetKeyDown(KeyCode.A)) { yield return DoubleAClick(); }
            else if(Input.GetKeyDown(KeyCode.S)) { yield return DoubleSClick(); }
            else if(Input.GetKeyDown(KeyCode.D)) { yield return DoubleDClick(); }

            yield return null;
        }
    }
    private IEnumerator DoubleWClick()
    {
        yield return new WaitForEndOfFrame();
        float count = 0f;
        while (count < dashCount)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(!Main_Player.Instance.isDash)
                sm.ChangeState(sm.dashstates[0]);
                yield break;
            }
            count += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator DoubleAClick()
    {
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while (count < dashCount)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (!Main_Player.Instance.isDash)
                    sm.ChangeState(sm.dashstates[1]);
                yield break;
            }
            count += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator DoubleSClick()
    {
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while (count < dashCount)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (!Main_Player.Instance.isDash)
                    sm.ChangeState(sm.dashstates[2]);
                yield break;
            }
            count += Time.deltaTime;
            yield return null;
        }
    }
    private IEnumerator DoubleDClick()
    {
        yield return new WaitForEndOfFrame();

        float count = 0f;
        while (count < dashCount)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (!Main_Player.Instance.isDash)
                    sm.ChangeState(sm.dashstates[3]);
                yield break;
            }
            count += Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}
