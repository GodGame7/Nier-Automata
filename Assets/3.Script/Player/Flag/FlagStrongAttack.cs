using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagStrongAttack1 : IFlagState
{
    public void Action()
    {
        Debug.Log("������ 1�޺� ��");
    }
}
public class FlagStrongAttack2 : IFlagState
{
    public void Action()
    {
        Debug.Log("������ 2�޺� ��");
    }
}
