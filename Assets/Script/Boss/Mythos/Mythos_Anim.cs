using UnityEngine;

public class Mythos_Anim : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // 애니메이션 재생 메서드
    public void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName);
        }
    }
}
