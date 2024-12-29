using UnityEngine;

public class AnimationChecker : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator ������Ʈ�� �� ������Ʈ�� �����ϴ�.");
        }
    }

    public bool IsAnimationFinished(string animationName)
    {
        if (animator == null) return false;

        // ���� ��� ���� �ִϸ��̼� ���� ������ �����ɴϴ�.
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼� �̸��� ��ġ�ϰ�, NormalizedTime�� 1 �̻��̸� �ִϸ��̼��� ���� ���Դϴ�.
        if (stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f)
        {
            return true;
        }

        return false;
    }
}
