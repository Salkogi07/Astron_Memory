using UnityEngine;

public class Mythos_Move : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트를 참조
    public float speed = 2f; // 이동 속도
    public bool isAttack = false;

    private Mythos mythos;

    private void Awake()
    {
        mythos = GetComponent<Mythos>();
    }

    void Update()
    {
        if (player != null && !isAttack && mythos.isMove)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (!isAttack)
        {
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;

            // 이동 방향에 따라 스프라이트 반전
            if (direction.x < 0)
            {
                // 오른쪽으로 이동하면 스프라이트 반전 없음
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x > 0)
            {
                // 왼쪽으로 이동하면 스프라이트 반전
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // 이동
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
