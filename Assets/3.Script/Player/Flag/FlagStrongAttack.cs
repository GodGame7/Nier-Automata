using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagStrongAttack1 : IFlagState
{
    public void Action()
    {
        Debug.Log("강공격 1콤보 중");
    }
}
public class FlagStrongAttack2 : IFlagState
{
    public void Action()
    {
        Debug.Log("강공격 2콤보 중");
    }
}
