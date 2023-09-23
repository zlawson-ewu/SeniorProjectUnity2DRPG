using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] Animator animator;
    bool facingRight = true;

    private void FixedUpdate()
    {
        if (Mathf.Abs(rb.velocity.x) < 0.1f)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
            if (rb.velocity.x > 0 && !facingRight)
            {
                flip();
            }
            else if (rb.velocity.x < 0 && facingRight)
            {
                flip();
            }
        }
    }

    void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}
