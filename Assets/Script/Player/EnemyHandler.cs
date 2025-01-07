using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public float searchRadius = 5f; // 탐색 반경

    void Update()
    {
        // 특정 범위 내의 오브젝트 탐색
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius);

        // Enemy 레이어를 가진 모든 오브젝트를 검색
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy"); // 모든 Enemy 찾기

        foreach (GameObject enemy in allEnemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance <= searchRadius)
            {
                // 범위 안의 오브젝트 처리
                enemy.GetComponent<Renderer>().enabled = true;

                Transform enemySmoke = enemy.transform.FindChildWithTag("EnemySmoke");
                if (enemySmoke != null)
                {
                    enemySmoke.gameObject.SetActive(false); // EnemySmoke 비활성화
                }
            }
            else
            {
                // 범위 밖의 오브젝트 처리
                enemy.GetComponent<Renderer>().enabled = false;

                Transform enemySmoke = enemy.transform.FindChildWithTag("EnemySmoke");
                if (enemySmoke != null)
                {
                    enemySmoke.gameObject.SetActive(true); // EnemySmoke 활성화
                }
            }
        }
    }
}

public static class TransformExtensions
{
    // 자식에서 특정 태그를 가진 오브젝트를 찾는 확장 메서드
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
