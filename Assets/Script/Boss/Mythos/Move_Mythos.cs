using UnityEngine;

public class Move_Mythos : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ�� ����
    public float speed = 2f; // �̵� �ӵ�
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

            // �̵� ���⿡ ���� ��������Ʈ ����
            if (direction.x < 0)
            {
                // ���������� �̵��ϸ� ��������Ʈ ���� ����
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x > 0)
            {
                // �������� �̵��ϸ� ��������Ʈ ����
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // �̵�
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
