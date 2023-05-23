using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    private bool isCombo = false;

    public void Dash(FlagControl player)
    {
        throw new System.NotImplementedException();
    }

    public void StrongAttack(FlagControl player)
    {
        player.anim.SetTrigger(player.hashGundamStrongAttack);
    }

    public void WeakAttack(FlagControl player, bool isHorizontal)
    {
        if (!isCombo)
        {
            player.anim.SetTrigger(player.hashGundamWeakAttack1);
            isCombo = true;
        }
        else
        {
            player.anim.SetTrigger(player.hashGundamWeakAttack2);
            isCombo = false;
        }
    }
}
