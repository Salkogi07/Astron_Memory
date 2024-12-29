using UnityEngine;

public class AnimationChecker : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 이 오브젝트에 없습니다.");
        }
    }

    public bool IsAnimationFinished(string animationName)
    {
        if (animator == null) return false;

        // 현재 재생 중인 애니메이션 상태 정보를 가져옵니다.
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // 애니메이션 이름이 일치하고, NormalizedTime이 1 이상이면 애니메이션이 끝난 것입니다.
        if (stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f)
        {
            return true;
        }

        return false;
    }
}
