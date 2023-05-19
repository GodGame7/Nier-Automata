using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAttack : IFlagState
{
    public void Action()
    {
        Debug.Log("공격 상태 진입");
    }
}
public class FlagNomal : IFlagState
{
    public void Action()
    {
        Debug.Log("노멀 상태 진입");
    }
}
public class FlagDash : IFlagState
{
    public void Action()
    {
        Debug.Log("대쉬 상태 진입");
    }

    public void SetAni(FlagControl player)
    {
        player.anim.SetTrigger(player.hashDash);
    }
}
