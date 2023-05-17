using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagWeakAttack : IFlagState
{
    public void Action()
    {
        Debug.Log("약공격중");
    }
}
