using UnityEngine;

public class Mythos_Anim : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // �ִϸ��̼� ��� �޼���
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
