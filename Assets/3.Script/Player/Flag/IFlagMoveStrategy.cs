using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFlagMoveStrategy
{
    public abstract void Move(FlagControl player);
}
