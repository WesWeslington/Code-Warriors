using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/EnemyDefinition", order = 1)]
public class EnemyDef : ScriptableObject
{
    [Header("Enemy Structure")]
    [SerializeField]
    private EnemySpecies enemySpec = EnemySpecies.Humanoid;

    [Header("Enemy Image Assets")]
    [SerializeField]
    private Sprite eHead = null;
    [SerializeField]
    private Sprite eBody = null;

    [Header("Enemy Animations")]
    [SerializeField]
    private List<AnimationClip> anims = new List<AnimationClip>();
}

public enum EnemySpecies
{
    Humanoid,
    Fish
}

