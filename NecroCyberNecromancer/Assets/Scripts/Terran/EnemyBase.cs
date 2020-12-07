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

    [SerializeField]
    MoveType moveState = MoveType.Stopped;
    float moveTimeout = 1;
    [SerializeField]
    float attackTimeRemaining = 0;
    float idleTimeRemaining = 0;

    bool isAngry = false;

    float direction = 0;

    [SerializeField]
    Transform playerRef = null;

    [SerializeField]
    float health = 1;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(moveState != MoveType.Stopped)
        {
            CalculateSpeed();
            
            CheckPathEnd();
        }

        if (Vector3.Distance(this.transform.position, playerRef.position) <= definition.AggressionRadius)
        {
            isAngry = true;
            MoveTo(playerRef.position);
            BasicAttack();
        }
        else { isAngry = false; }

        if(!isAngry && moveState == MoveType.Stopped)
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

        health = definition.Health;
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
            moveState = MoveType.Stopped;
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
        if(moveState == MoveType.Stopped)
        {
            if(idleTimeRemaining <= 0)
            {
                idleTimeRemaining = Random.Range(definition.IdleDelay.x, definition.IdleDelay.y);
                if (patrolIndex < patrolPoints.Count-1)
                {
                    patrolIndex++;
                    MoveTo(patrolPoints[patrolIndex].position + new Vector3(Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius), patrolPoints[patrolIndex].position.y, Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius)));
                }else
                {
                    patrolIndex=0;
                    MoveTo(patrolPoints[patrolIndex].position + new Vector3(Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius), patrolPoints[patrolIndex].position.y, Random.Range(-Definition.PatrolRadius, Definition.PatrolRadius)));
                }
            }
        }else if(moveState != MoveType.Stopped && !navAgent.hasPath)
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
            idleTimeRemaining = Random.Range(definition.IdleDelay.x, definition.IdleDelay.y);
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
        navAgent.isStopped = false;
        animator.SetBool("IsMoving", true);
        //print("Setting new path");
        //StartCoroutine(MoveLimitTimer());
    }

    public virtual void CheckPathEnd()
    {
        //print(Vector3.Distance(navAgent.pathEndPosition, this.transform.position));
        if (navAgent.pathEndPosition != null && Vector3.Distance(navAgent.pathEndPosition, this.transform.position) <= navAgent.stoppingDistance + 0.25f && moveState != MoveType.Stopped)
        {
            //print("Stopping path");
            navAgent.isStopped = true;
            moveState = MoveType.Stopped;
            animator.SetBool("IsMoving", false);
            //StopCoroutine(MoveLimitTimer());
        }
    }

    public virtual void BasicAttack()
    {
        //print("Enemy Attacking");
        if(Vector3.Distance(this.transform.position,playerRef.position) <= definition.AttackRange && attackTimeRemaining <= 0)
        {
            navAgent.isStopped = true;
            moveState = MoveType.Stopped;

            animator.SetTrigger("Attacking");
            SetDirection(GetDirection(playerRef.position));
        }
    }

    public virtual void SpawnProjectile()
    {

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

    void DoAttackTimer()
    {
        if (!isAngry) { return; }

        if (attackTimeRemaining <= 0)
        {
            attackTimeRemaining = -1f;
        }
        else
        {
            attackTimeRemaining -= Time.deltaTime;
        }
    }

    int GetDirection(Vector3 _target)
    {
        int _direction = 0;

        Vector3 targetDir = _target - transform.position;
        float _targetAngle = Vector3.Angle(targetDir, transform.forward) * 2;
        print(_targetAngle);

        if (_targetAngle >= 45 && _targetAngle < 135)
        {
            print("Looking Right");
            _direction = 1;
        }else if(_targetAngle >= 135 && _targetAngle < 225)
        {
            print("Looking Up");
            _direction = 2;
        }else if (_targetAngle >= 225 && _targetAngle < 315)
        {
            print("Looking Left");
            _direction = 3;
        }

        return _direction;
    }

    void SetDirection(int _dir)
    {
        direction = _dir;
        animator.SetInteger("Dir", _dir);
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
