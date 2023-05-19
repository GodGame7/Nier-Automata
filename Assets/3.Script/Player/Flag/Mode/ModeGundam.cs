using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeGundam : IFlagModeStrategy
{
    private bool isCombo = false;
    public void StrongAttack(FlagControl player)
    {
        Debug.Log("�Ǵ� ������");
    }

    public void WeakAttack(FlagControl player)
    {
        if (!isCombo)
        {
            Debug.Log("�Ǵ� ��� 1��");
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
        Debug.Log("�Ǵ� ��� 2��");
    }
}
