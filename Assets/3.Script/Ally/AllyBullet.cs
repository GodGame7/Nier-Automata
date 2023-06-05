using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBullet : FlagBullet
{
    [SerializeField]
    private float allyDamage = 2f;

    private void Start()
    {
        damage = allyDamage;
    }

    protected new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
