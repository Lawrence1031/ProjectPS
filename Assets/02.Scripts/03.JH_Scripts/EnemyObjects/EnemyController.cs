using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyAnimationCotroller
{
    public EnemyData enemyData;
    public LayerMask wallLayerMask;

    private bool _isMove = true;
    private Rigidbody _rigidbody;

    private readonly int isWalking = Animator.StringToHash("IsWalking");

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_isMove)
        {
            Move();
        }
       
    }

    private void Move()
    {
        animator.SetBool(isWalking, true);

        float moveSpeed = enemyData.walkSpeed;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);


        if (IsWall())
        {
            StopMove();
        }

    }

    private void StopMove()
    {
        if( _rigidbody != null )
        {
            _rigidbody.velocity = Vector3.zero;
            animator.SetBool(isWalking, false);

        }
        _isMove = false;

        StartCoroutine(EnemyCoroutine());
    }

    private IEnumerator EnemyCoroutine()
    {
        yield return new WaitForSecondsRealtime(3f);

        /// 게임 오브젝트의 rotation 값
        Vector3 currentRotation = gameObject.transform.rotation.eulerAngles;

        /// 
        Vector3 targetRotation = new Vector3(currentRotation.x, currentRotation.y, currentRotation.z);

        /// 회전 구현
        if (currentRotation.y == 90f)
        {
             targetRotation = new Vector3(currentRotation.x, 270f, currentRotation.z);
        }
        else
        {
             targetRotation = new Vector3(currentRotation.x, 90f, currentRotation.z);
        }

        float time = 0f;
        while (time < 1f)
        {
            animator.SetBool(isWalking, false);
            time += Time.deltaTime / 2f; // 2초 동안 보간
            gameObject.transform.rotation = Quaternion.Euler(Vector3.Lerp(currentRotation, targetRotation, time));
            yield return null;
        }

        _isMove = true;
    }

    /// <summary>
    /// Ray
    /// </summary>
    /// <returns></returns>
    public bool IsWall()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.up * 2.0f) + (Vector3.forward * 0.01f), transform.forward * 5f),
            new Ray(transform.position + (transform.up * 2.0f) + (Vector3.forward * 0.01f), transform.forward * 5f),
            new Ray(transform.position + (transform.right * 0.2f) + (Vector3.forward * 0.01f), transform.forward * 5f),
            new Ray(transform.position + (transform.right * -0.2f) + (Vector3.forward * 0.01f), transform.forward * 5f),
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1f, wallLayerMask)) 
            {
                Debug.Log("wall?");
                return true;
            }
        }

        return false;
     
    }


    /// <summary>
    /// Gizmos
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + (transform.up * 2.5f), transform.forward * 5f);
        Gizmos.DrawRay(transform.position + (transform.up * 2.5f), transform.forward * 5f);
        Gizmos.DrawRay(transform.position + (transform.right * 0.2f), transform.forward * 5f);
        Gizmos.DrawRay(transform.position + (transform.right * -0.2f), transform.forward * 5f);
        //Gizmos.DrawRay(transform.position + (-transform.forward * 0.2f), Vector3.forward);
        //Gizmos.DrawRay(transform.position + (transform.right * 0.2f), Vector3.forward);
        //Gizmos.DrawRay(transform.position + (-transform.right * 0.2f), Vector3.forward);
    }


}
