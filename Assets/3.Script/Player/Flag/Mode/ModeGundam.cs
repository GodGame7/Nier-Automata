using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    public void StrongAttack(FlagControl player)
    {
        Debug.Log("건담 강공격 중");
    }

    public void WeakAttack1(FlagControl player)
    {
        Debug.Log("건담 약공 1단 중");
    }

    public void WeakAttack2(FlagControl player)
    {
        Debug.Log("건담 약공 2단 중");
    }
}
