using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeFlag : IFlagModeStrategy
{
    public void StrongAttack(FlagControl player)
    {
        Debug.Log("비행기 강공격");
    }

    public void WeakAttack(FlagControl player)
    {
        Debug.Log("비행기 약공격");
    }
}
