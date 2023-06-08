using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public enum BGM
    {
        Flag, Intro,
    }

    public enum SFX
    {
        EnemyShot, Raser, GundamAttack, ToFlag, FlagBarrier, FlagAttack, Shot, Hit, ToGundam, Dash, Danger, 
        Atk1, Atk2, Atk3, Atk4, AtkStrong, Dash2, Dodge, Charge, Title_Warning , UI_Enter,
        UI_Move , UI_Space

    }

    public enum EnemyTagType
    {
        Enemy, BulletSoft, BulletHard,
    }
}
