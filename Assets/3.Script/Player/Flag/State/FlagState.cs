using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagAttack : IFlagState
{
    public void Action()
    {
        Debug.Log("���� ���� ����");
    }
}
public class FlagNomal : IFlagState
{
    public void Action()
    {
        Debug.Log("��� ���� ����");
    }
}
public class FlagDash : IFlagState
{
    public void Action()
    {
        Debug.Log("�뽬 ���� ����");
    }

    public void SetAni(FlagControl player)
    {
        player.anim.SetTrigger(player.hashDash);
    }
}
