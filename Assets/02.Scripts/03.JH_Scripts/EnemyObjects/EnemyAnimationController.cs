using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationCotroller : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

}
