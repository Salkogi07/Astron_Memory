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
        Debug.Log(animator);
        if (animator != null)
        {
            animator.Play(animationName);
            Debug.Log(animationName);
        }
    }
}
