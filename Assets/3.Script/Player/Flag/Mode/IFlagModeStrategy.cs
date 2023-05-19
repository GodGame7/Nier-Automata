using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlagModeStrategy
{
    public void WeakAttack(FlagControl player);
    public void StrongAttack(FlagControl player);
}
