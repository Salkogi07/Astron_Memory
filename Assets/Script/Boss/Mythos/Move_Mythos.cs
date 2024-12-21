using UnityEngine;

public class Move_Mythos : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트를 참조
    public float speed = 2f; // 이동 속도
    public bool isAttack = false;

    void Update()
    {
        if (player != null && !isAttack)
        {
            MoveTowardsPlayer();
        }
    }

    void MoveTowardsPlayer()
    {
        if (!isAttack)
        {
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
