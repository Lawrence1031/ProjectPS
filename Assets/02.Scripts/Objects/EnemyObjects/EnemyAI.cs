using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // AI 사용

public enum AIState
{
    IDLE,
    WANDERING,
    ATTACKING,
    FLEEING
}


public class EnemyAI : EnemyAnimationCotroller
{
    public PlayerController player;
    public EnemyData enemyData;

    [Header("AI")]
    private AIState aiState;
    public LayerMask wallLayerMask;
    public float detectDistance;
    public float safeDistance;



    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")]
    public float attackRate;
    public float attackDistance;
    private float lastAttackTime;
    private float playerDistance;

    public float filedOfView = 60f;

    private NavMeshAgent agent;
    private Rigidbody _rigidbody;

    private readonly int isWalking = Animator.StringToHash("IsWalking");
    private readonly int IsRunning = Animator.StringToHash("IsRunning");
    private readonly int isAttaking = Animator.StringToHash("Attacking");

    private Vector3 playerPosition;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetState(AIState.WANDERING);
        enemyData.target = Target.Player;
    }

    private void Update()
    {
        GameObject targetObject = GetTargetPlayer(enemyData.target);
        playerPosition = targetObject.transform.position;
        playerDistance = Vector3.Distance(transform.position, playerPosition);

        animator.SetBool(isWalking, aiState != AIState.IDLE);

        switch(aiState)
        {
            case AIState.IDLE: PassiveUpdate(); break;
            case AIState.WANDERING: PassiveUpdate(); break;
            case AIState.ATTACKING: AttackingUpdate(); break;
            case AIState.FLEEING: FleeingUpdate(); break;
        }

    }

    /// <summary>
    /// 타겟을 플레이어로 지정
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private GameObject GetTargetPlayer(Target target)
    {
        GameObject targetObject = null;

        if (target == Target.Player)
        {
            targetObject = GameObject.FindGameObjectWithTag("Player");
        }

        return targetObject;

    }

    /// <summary>
    /// 어떠한 상태를 줬을 때 그 상태를 적용하기 위해서 만들어 주는 것
    /// </summary>
    /// <param name="newState"></param>
    private void SetState(AIState newState)
    {
        aiState = newState;
        switch (aiState)
        {
            case AIState.IDLE:
                {
                    agent.speed = enemyData.walkSpeed;
                    agent.isStopped = true;
                }
                break;
            case AIState.WANDERING:
                {
                    agent.speed = enemyData.walkSpeed;
                    agent.isStopped = false;
                }
                break;

            case AIState.FLEEING:
                {
                    agent.speed = enemyData.runSpeed;
                    agent.isStopped = false;
                }
                break;
            case AIState.ATTACKING:
                {
                    agent.speed = enemyData.runSpeed;
                    agent.isStopped = false;
                }
                break;
        }

        animator.speed = agent.speed / enemyData.walkSpeed;

    }

    private void FleeingUpdate()
    {
        if (agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(GetFleeLocation());
        }
        else
        {
            SetState(AIState.WANDERING);
        }
    }
   

    /// <summary>
    /// 공격중
    /// </summary>
    private void AttackingUpdate()
    {
        // 공격중일때 플레이어가 멀어진다면
        if (playerDistance > attackDistance || !IsPlayerFieldOfView())
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();

            // 경로체크하고 플레이어 포지션이 이동 가능한 경로면 이동한다.
            if (agent.CalculatePath(playerPosition, path))
            {
                agent.SetDestination(playerPosition);
                animator.SetBool(IsRunning, aiState != AIState.IDLE);
            }   
            else
            {
                // 아니면 Fleeing
                SetState(AIState.FLEEING);
            }
        }
        else
        {
            agent.isStopped = true;
            //SetState(AIState.IDLE);
            if (Time.time - lastAttackTime > attackRate)
            {
                transform.LookAt(player.transform);
                lastAttackTime = Time.time;
                player.GetComponent<IDamagable>().TakePhysicalDamage(enemyData.damage);
                animator.speed = 1;
                animator.SetTrigger(isAttaking);

            }
        }
    }

    // 시야 (눈에 들어오는 적만 공격)
    private bool IsPlayerFieldOfView()
    {
        Vector3 directionToPlayer = playerPosition - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < filedOfView * 0.5f;

    }

    private void PassiveUpdate()
    {
        // 방황하는데 남은 거리가 짧을 경우
        if (aiState == AIState.WANDERING && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.IDLE);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
            animator.SetBool(IsRunning, false);
        }

        // 인지하는 거리에 들어올 때 
        if (playerDistance < detectDistance)
        {
            SetState(AIState.ATTACKING);
        }

        //if(IsWall())
        //{
        //    StopMove();
        //}

    }
    /// <summary>
    /// invoke
    /// </summary>
    void WanderToNewLocation()
    {
        if (aiState != AIState.IDLE)
        {
            return;
        }

        SetState(AIState.WANDERING);
        agent.SetDestination(GetWanderLocation());
    }

    private Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere) * Random.Range(minWanderDistance, maxWanderDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere) * Random.Range(minWanderDistance, maxWanderDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;

            if (i == 30)
            {
                break;
            }
        }

        return hit.position;

    }
    private Vector3 GetFleeLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere) * safeDistance, out hit, maxWanderDistance, NavMesh.AllAreas);
        

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere) * safeDistance, out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;

            if (i == 30)
            {
                break;
            }
        }

        return hit.position;
    }

    /// <summary>
    /// 도망가는 경로를 찾음
    /// </summary>
    /// <param name="targetPos"></param>
    /// <returns></returns>
    private float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - playerPosition, transform.position + targetPos);
    }

    //private void StopMove()
    //{
    //    if (_rigidbody != null)
    //    {
    //        _rigidbody.velocity = Vector3.zero;
    //        SetState(AIState.IDLE);

    //    }

    //    StartCoroutine(TurnToWall());
    //}

    //private IEnumerator TurnToWall()
    //{
    //    yield return new WaitForSecondsRealtime(2f);
    //    /// 게임 오브젝트의 rotation 값
    //    Vector3 currentRotation = gameObject.transform.rotation.eulerAngles;

    //    Vector3 targetRotation = new Vector3(currentRotation.x, Mathf.Abs(currentRotation.y - 180f), currentRotation.z);

    //    // 타겟을 돌림
    //    transform.rotation = Quaternion.Euler(targetRotation);

    //    SetState(AIState.WANDERING);

    //}

    ///// <summary>
    ///// Ray
    ///// </summary>
    ///// <returns></returns>
    //public bool IsWall()
    //{
    //    Ray[] rays = new Ray[4]
    //    {
    //        new Ray(transform.position + (transform.up * 2.0f) + (Vector3.forward * 0.01f), transform.forward * 5f),
    //        new Ray(transform.position + (transform.up * 2.0f) + (Vector3.forward * 0.01f), transform.forward * 5f),
    //        new Ray(transform.position + (transform.right * 0.2f) + (Vector3.forward * 0.01f), transform.forward * 5f),
    //        new Ray(transform.position + (transform.right * -0.2f) + (Vector3.forward * 0.01f), transform.forward * 5f),
    //    };

    //    for (int i = 0; i < rays.Length; i++)
    //    {
    //        if (Physics.Raycast(rays[i], 1f, wallLayerMask))
    //        {
    //            Debug.Log("wall?");
    //            return true;
    //        }
    //    }

    //    return false;

    //}


    ///// <summary>
    ///// Gizmos
    ///// </summary>
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawRay(transform.position + (transform.up * 2.5f), transform.forward * 5f);
    //    Gizmos.DrawRay(transform.position + (transform.up * 2.5f), transform.forward * 5f);
    //    Gizmos.DrawRay(transform.position + (transform.right * 0.2f), transform.forward * 5f);
    //    Gizmos.DrawRay(transform.position + (transform.right * -0.2f), transform.forward * 5f);
    //    //Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.forward);
    //    //Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.forward);
    //    //Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.forward);
    //}

}
