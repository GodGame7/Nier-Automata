using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeFlag : IFlagModeStrategy
{
    public void StrongAttack(FlagControl player)
    {
        Debug.Log("����� ������");
    }

    public void WeakAttack(FlagControl player)
    {
        Debug.Log("����� �����");
    }
}
