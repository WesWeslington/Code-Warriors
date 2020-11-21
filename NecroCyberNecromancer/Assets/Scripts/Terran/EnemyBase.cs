using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    private EnemyDef definition = null;

    [SerializeField]
    Transform[] bodyStructures = null;

    public EnemyDef Definition
    {
        get { return definition; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
