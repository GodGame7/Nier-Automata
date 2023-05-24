using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    public void Dash(FlagControl player)
    {
        throw new System.NotImplementedException();
    }

    #region 공격
    public void StrongAttack(FlagControl player)
    {
        player.anim.SetTrigger(player.hashGundamStrongAttack);
    }
    public void WeakAttack(FlagControl player, bool isHorizontal)
    {
        if (!player.isCombo)
        {
            player.anim.SetTrigger(player.hashGundamWeakAttack1);
            player.isCombo = true;
        }
        else
        {
            player.anim.SetTrigger(player.hashGundamWeakAttack2);
            player.isCombo = false;
        }
    }
    #endregion 공격
}
