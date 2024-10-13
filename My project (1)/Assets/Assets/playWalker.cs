using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.Play("DoctorClip");
    }
}
