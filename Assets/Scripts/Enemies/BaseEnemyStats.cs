using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/EnemyStats")]

public class BaseEnemyStats : ScriptableObject
{

    public float health;
    public float movespeed;

    public BulletPattern bulletPattern;
    public BulletComplexPattern complexBulletPattern;

    public float range;
    public float fireRateCooldown;
    public Transform patternDrop;

}
