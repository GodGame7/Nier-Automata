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
        BtnClick, BtnOver, //플레이어 효과음 이름 추가
    }

    public enum EnemyTagType
    {
        Enemy, BulletSoft, BulletHard,
    }
}
