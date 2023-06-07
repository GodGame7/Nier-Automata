using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public enum BGM
    {
        Flag, MainField,
    }

    public enum SFX
    {
        EnemyShot, Raser, GundamAttack, ToFlag, FlagBarrier, FlagAttack, Shot, Hit, ToGundam, Dash, Danger, Atk1, Atk2, Atk3, Atk4
    }

    public enum EnemyTagType
    {
        Enemy, BulletSoft, BulletHard,
    }
}
