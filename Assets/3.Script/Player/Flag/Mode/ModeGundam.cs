using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    public void StrongAttack(FlagControl player)
    {
        Debug.Log("�Ǵ� ������ ��");
    }

    public void WeakAttack1(FlagControl player)
    {
        Debug.Log("�Ǵ� ��� 1�� ��");
    }

    public void WeakAttack2(FlagControl player)
    {
        Debug.Log("�Ǵ� ��� 2�� ��");
    }
}
