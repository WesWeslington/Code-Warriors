using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

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
    Transform projectileSpawner = null;

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

            if (navAgent.nextPosition != null)
            {
                SetDirection(GetDirection(navAgent.pathEndPosition));
            }
        }

        if (Vector3.Distance(this.transform.position, playerRef.position) <= definition.AggressionRadius)
        {
            isAngry = true;
            if (Vector3.Distance(this.transform.position, playerRef.position) > definition.AttackRange && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Right_Default") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Left_Default") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Up_Default") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Down_Default"))
            {
                MoveTo(playerRef.position);
            }else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Right_Default") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Left_Default") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Up_Default") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Down_Default"))
            {
                BasicAttack();
            }
            
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

        if (projectileSpawner == null)
        { projectileSpawner = this.transform; }

        if (patrolPoints.Count == 0)
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
        if(Vector3.Distance(this.transform.position,playerRef.position) <= definition.AttackRange)
        {
            navAgent.isStopped = true;
            moveState = MoveType.Stopped;

            SetDirection(GetDirection(playerRef.position));
            animator.SetTrigger("Attacking");
            print("Attacking!");
        }
    }

    public virtual void SpawnEnemyProjectile(int _i)
    {
        Transform newProjectile = Instantiate(definition.Projectiles[_i], projectileSpawner.position, Quaternion.identity);
        EnemyProjectile _proj = newProjectile.GetComponent<EnemyProjectile>();
        _proj.projectileDamage = definition.Damage;
        if (animator.GetInteger("Dir") == 3)
        {
            _proj.FlipSprite();
        }
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
            animator.ResetTrigger("Attacking");
        }
        else
        {
            attackTimeRemaining -= Time.deltaTime;
        }
    }

    int GetDirection(Vector3 _target)
    {
        int _direction = 0;

        //Vector3 targetDir = _target - transform.position;
        //float _targetAngle = Vector3.AngleBetween(Vector3.up, _target) * 2;
        

        float _targetAngle = (Mathf.Atan2(transform.position.x-_target.x, transform.position.z - _target.z) / Mathf.PI) * 180f;
        if (_targetAngle < 0) { _targetAngle += 360f; }
        //print(_targetAngle);

        if (_targetAngle >= 45 && _targetAngle < 135)
        {
            //print("Looking Left");
            _direction = 3;
        }else if(_targetAngle >= 135 && _targetAngle < 225)
        {
            //print("Looking Up");
            _direction = 2;
        }else if (_targetAngle >= 225 && _targetAngle < 315)
        {
            //print("Looking Right");
            _direction = 1;
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
