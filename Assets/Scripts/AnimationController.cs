using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        PlayerMovement.AnimationEvent += SetAnim;
    }

    void OnDisable()
    {
        PlayerMovement.AnimationEvent -= SetAnim;
    }

    void SetAnim(Vector2 dir, bool isJumpPlatform)
    {
        if(dir.x == -1)
        {
            if(isJumpPlatform)
            {
                animator.SetTrigger("Left");
                animator.SetTrigger("JumpFloor");
            }
            else
            {
                animator.SetTrigger("Left");
                animator.SetTrigger("Floor");
            }
        }
        else if (dir.x == 1)
        {
            if (isJumpPlatform)
            {
                animator.SetTrigger("Right");
                animator.SetTrigger("JumpFloor");
            }
            else
            {
                animator.SetTrigger("Right");
                animator.SetTrigger("Floor");
            }
        }
        else if (dir.y == 1)
        {
            if (isJumpPlatform)
            {
                animator.SetTrigger("Front");
                animator.SetTrigger("JumpFloor");
            }
            else
            {
                animator.SetTrigger("Front");
                animator.SetTrigger("Floor");
            }
        }
        else if (dir.y == -1)
        {
            if (isJumpPlatform)
            {
                animator.SetTrigger("Back");
                animator.SetTrigger("JumpFloor");
            }
            else
            {
                animator.SetTrigger("Back");
                animator.SetTrigger("Floor");
            }
        }
    }
}
