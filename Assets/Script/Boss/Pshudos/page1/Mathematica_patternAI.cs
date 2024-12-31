using UnityEngine;

public class PufferfishSpawner : MonoBehaviour
{
    // ���� �������� �����մϴ�.
    public GameObject pufferfishPrefab;
    

    // ��� ��ȯ�� ��ġ�� �����մϴ�.
    public Vector3[] spawnPositions;

    void Start()
    {
        // ��ȯ ��ġ�� 3������ Ȯ���մϴ�.
        if (spawnPositions.Length < 3)
        {
            Debug.LogError("��ȯ ��ġ�� ������� �ʽ��ϴ�. �ּ� 3���� ��ġ�� �ʿ��մϴ�.");
            return;
        }

        // ��� �� 3���� ��ȯ�մϴ�.
        for (int i = 0; i < 3; i++)
        {
            SpawnPufferfish(spawnPositions[i]);
        }
    }

    // ��� Ư�� ��ġ�� ��ȯ�ϴ� �Լ�
    void SpawnPufferfish(Vector3 position)
    {
        if (pufferfishPrefab != null)
        {
            Instantiate(pufferfishPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Pufferfish Prefab�� �������� �ʾҽ��ϴ�.");
        }
    }
}

