using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeFlag : IFlagModeStrategy
{
    public void StrongAttack(FlagControl player)
    {
        Debug.Log("����� ������ ��");
    }

    public void WeakAttack1(FlagControl player)
    {
        Debug.Log("����� ��� 1�� ��");
    }

    public void WeakAttack2(FlagControl player)
    {
        Debug.Log("����� ��� 2�� ��");
    }
}
