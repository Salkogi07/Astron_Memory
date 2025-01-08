using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public float searchRadius = 5f; // Ž�� �ݰ�

    void Update()
    {
        // Ư�� ���� ���� ������Ʈ Ž��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);

        // Enemy ���̾ ���� ��� ������Ʈ�� �˻�
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy"); // ��� Enemy ã��

        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= searchRadius)
            {
                // ���� ���� ������Ʈ ó��
                enemy.GetComponent<Renderer>().enabled = true;

                Transform enemySmoke = enemy.transform.FindChildWithTag("EnemySmoke");
                if (enemySmoke != null)
                {
                    enemySmoke.gameObject.SetActive(false); // EnemySmoke ��Ȱ��ȭ
                }
            }
            else
            {
                // ���� ���� ������Ʈ ó��
                enemy.GetComponent<Renderer>().enabled = false;

                Transform enemySmoke = enemy.transform.FindChildWithTag("EnemySmoke");
                if (enemySmoke != null)
                {
                    enemySmoke.gameObject.SetActive(true); // EnemySmoke Ȱ��ȭ
                }
            }
        }
    }
}

public static class TransformExtensions
{
    // �ڽĿ��� Ư�� �±׸� ���� ������Ʈ�� ã�� Ȯ�� �޼���
    public static Transform FindChildWithTag(this Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
                return child;
        }
        return null;
    }
}
