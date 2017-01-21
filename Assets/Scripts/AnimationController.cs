using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

	void SetAnim(Vector2 dir)
    {
        if(dir.x == 1)
        {
            animator.SetTrigger("Left");
        }
        else if (dir.x == -1)
        {
            animator.SetTrigger("Right");
        }
        else if (dir.y == 1)
        {
            animator.SetTrigger("Front");
        }
        else if (dir.y == -1)
        {
            animator.SetTrigger("Back");
        }
    }
}
