using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlagModeStrategy
{
    public void WeakAttack1(FlagControl player);
    public void WeakAttack2(FlagControl player);
    public void StrongAttack(FlagControl player);
}
