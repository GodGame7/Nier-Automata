using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    private bool isCombo = false;
    public void StrongAttack(FlagControl player)
    {
        Debug.Log("건담 강공격");
    }

    public void WeakAttack(FlagControl player)
    {
        if (!isCombo)
        {
            Debug.Log("건담 약공 1단");
            isCombo = true;
        }
        else
        {
            WeakAttack2(player);
            isCombo = false;
        }
    }

    private void WeakAttack2(FlagControl player)
    {
        Debug.Log("건담 약공 2단");
    }
}
