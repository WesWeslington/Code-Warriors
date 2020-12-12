using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/EnemyDefinition", order = 1)]
public class EnemyDef : ScriptableObject
{
    [Header("Enemy Structure")]
    [SerializeField]
    private EnemySpecies enemySpec = EnemySpecies.Humanoid;

    [Header("Enemy Display")]
    [SerializeField]
    private EnemySprites enemySprites = null;

    [Header("Enemy Animations")]
    [SerializeField]
    private AnimatorOverrideController enemyAnimations = null;

    [Header("Enemy Stats")]
    [SerializeField]
    private float baseHP = 10;
    [SerializeField]
    private float speedWalk = 1;
    [SerializeField]
    private float speedRun = 3;
    [SerializeField]
    private float damage = 1;
    [SerializeField]
    private float attackDelay = 1;
    [SerializeField]
    private float attackRange = 2;
    [SerializeField]
    private float aggressionRadius = 5;

    [SerializeField]
    private float idleMaxDelay = 1;
    [SerializeField]
    private IdleType idleType = IdleType.Sleep;
    [SerializeField]
    private float patrolRadius = 10;

    [SerializeField]
    private List<Transform> projectiles = new List<Transform>();

    public EnemySprites GetSprites
    {
        get { return enemySprites; }
    }

    public AnimatorOverrideController GetAnimations
    {
        get { return enemyAnimations; }
    }

    public float Health
    {
        get { return baseHP; }
    }
    public float SpeedWalk
    {
        get { return speedWalk; }
    }
    public float SpeedRun
    {
        get { return speedRun; }
    }
    public float Damage
    {
        get { return damage; }
    }
    public float AttackRange
    {
        get { return attackRange; }
    }
    public float AttackDelay
    {
        get { return attackDelay; }
    }
    public float AggressionRadius
    {
        get { return aggressionRadius; }
    }
    public float PatrolRadius
    {
        get { return patrolRadius; }
    }
    /// <summary>
    /// Returns a Vector2 where X is the minimum and Y is the maximum value.
    /// </summary>
    public Vector2 IdleDelay
    {
        get { return new Vector2(0,idleMaxDelay); }
    }
    public IdleType IdleType
    {
        get { return idleType; }
    }
    public List<Transform> Projectiles
    {
        get { return projectiles; }
    }

}

public enum EnemySpecies
{
    Humanoid,
    Fish
}

public enum IdleType
{
    Sleep,
    Guard,
    Patrol
}

