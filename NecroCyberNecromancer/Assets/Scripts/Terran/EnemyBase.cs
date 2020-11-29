using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private EnemyDef definition = null;

    [SerializeField]
    private EnemySprites visuals = null;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private Transform[] eSkeleton = null;

    public EnemyDef Definition
    {
        get { return definition; }
    }

    public EnemySprites Sprites
    {
        get { return visuals; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (animator == null)
        { animator = this.GetComponent<Animator>(); }
        animator.runtimeAnimatorController = definition.GetAnimations;
        visuals = definition.GetSprites;
        visuals.RenderEnemy(eSkeleton, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
