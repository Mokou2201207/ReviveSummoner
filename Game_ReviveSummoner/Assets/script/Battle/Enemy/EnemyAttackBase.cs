using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttackBase : MonoBehaviour
{
    [Header("o‚·‹Ê")]
    public GameObject bulletPrefab;
    // UŒ‚‚ğÀs‚·‚é–½—ß
    public abstract void ExecuteAttack(); 
}