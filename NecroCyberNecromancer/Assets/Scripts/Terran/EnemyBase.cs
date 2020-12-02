using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField]
    private NavMeshAgent navAgent = null;

    [SerializeField]
    private List<Transform> patrolPoints = new List<Transform>();
    int patrolIndex = 0;

    public EnemyDef Definition
    {
        get { return definition; }
    }

    public EnemySprites Sprites
    {
        get { return visuals; }
    }

    MoveType moveState = MoveType.Stopped;
    float moveTimeout = 1;
    float idleTimeRemaining = 0;

    bool isAngry = false;

    Transform playerRef = null;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            MoveTo(new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10)));
        }

        if(moveState != MoveType.Stopped)
        {
            CalculateSpeed();
        }

        if (Vector3.Distance(this.transform.position, playerRef.position) <= definition.AggressionRadius)
        {
            isAngry = true;
            MoveTo(playerRef.position);
        }
        else { isAngry = false; }

        if(!isAngry)
        {
            DoIdleTimer();
        }
    }

    public virtual void Initialize()
    {
        if (animator == null)
        { animator = this.GetComponent<Animator>(); }
        animator.runtimeAnimatorController = definition.GetAnimations;
        visuals = definition.GetSprites;
        visuals.RenderEnemy(eSkeleton, 0);

        if (navAgent == null)
        { navAgent = this.GetComponent<NavMeshAgent>(); }
        navAgent.speed = Definition.SpeedWalk;

        if(patrolPoints.Count == 0)
        {
            patrolPoints.Add(this.transform);
        }
    }

    float CalculateSpeed()
    {
        float _speed = 1;
        if (moveState == MoveType.Running)
        {
            _speed = definition.SpeedRun;
        }
        else if (moveState == MoveType.Walking)
        {
            _speed = definition.SpeedWalk;
        }else
        {
            _speed = 0;
        }

        navAgent.speed = _speed;
        return _speed;
    }

    void Idle()
    {
        if (definition.IdleType == IdleType.Patrol)
        {
            IdlePatrol();
        }else if(definition.IdleType == IdleType.Guard)
        {
            IdleGuard();
        }else
        {
            IdleSleep();
        }
    }

    void IdlePatrol()
    {
        if (Vector3.Distance(this.transform.position,patrolPoints[patrolIndex].position) <= navAgent.stoppingDistance)
        {
            if(idleTimeRemaining <= 0)
            {
                if(patrolIndex < patrolPoints.Count-1)
                {
                    patrolIndex++;
                    MoveTo(patrolPoints[patrolIndex].position + new Vector3(Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius), patrolPoints[patrolIndex].position.y, Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius)));
                }else
                {
                    patrolIndex=0;
                    MoveTo(patrolPoints[patrolIndex].position + new Vector3(Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius), patrolPoints[patrolIndex].position.y, Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius)));
                }
            }else
            {
                idleTimeRemaining = Random.Range(definition.IdleDelay.x, definition.IdleDelay.y);
            }
        }else if(!navAgent.hasPath)
        {
            patrolIndex = Random.Range(0, patrolPoints.Count);
            MoveTo(patrolPoints[patrolIndex].position + new Vector3(Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius), patrolPoints[patrolIndex].position.y, Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius)));
        }
    }

    void IdleGuard()
    {
        if (idleTimeRemaining <= 0 && navAgent.remainingDistance <= navAgent.stoppingDistance + 1)
        {
            MoveTo(patrolPoints[patrolIndex].position + new Vector3(Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius), patrolPoints[patrolIndex].position.y, Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius)));
        }
    }

    void IdleSleep()
    {

    }

    public virtual void MoveTo(Vector3 _target)
    {
        StopCoroutine(MoveLimitTimer());
        navAgent.SetDestination(_target);
        moveTimeout = (Vector3.Distance(_target, navAgent.pathEndPosition)/navAgent.speed) * 2f;
        if (isAngry) { moveState = MoveType.Running; } else { moveState = MoveType.Walking; }
        //StartCoroutine(MoveLimitTimer());
    }

    public virtual void CheckPathEnd()
    {
        if (navAgent.pathEndPosition != null && Vector3.Distance(navAgent.pathEndPosition, this.transform.position) <= navAgent.stoppingDistance + 0.25f)
        {
            navAgent.isStopped = true;
            //StopCoroutine(MoveLimitTimer());
        }
    }

    public virtual void BasicAttack()
    {
        print("Enemy Attacking");
    }

    public virtual void TakeDamage(float _dmg)
    {

    }

    public virtual void Stun(float _duration)
    {

    }

    void DoIdleTimer()
    {
        if (isAngry) { return; }

        if (idleTimeRemaining <= 0)
        {
            idleTimeRemaining = -1f;
            Idle();
        }else
        {
            idleTimeRemaining -= Time.deltaTime;
        }
    }

    //Not working for some reason but currently not needed either.
    public IEnumerator MoveLimitTimer()
    {
        yield return new WaitForSeconds(moveTimeout);
        CheckPathEnd();
        navAgent.isStopped = true;
        print("Path took too long to complete!");
    }
}

public enum MoveType
{
    Stopped,
    Walking,
    Running
}
