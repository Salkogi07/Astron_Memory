using UnityEngine;

public class PufferfishSpawner : MonoBehaviour
{
    // 복어 프리팹을 연결합니다.
    public GameObject pufferfishPrefab;
    

    // 복어를 소환할 위치를 저장합니다.
    public Vector3[] spawnPositions;

    void Start()
    {
        // 소환 위치가 3개인지 확인합니다.
        if (spawnPositions.Length < 3)
        {
            Debug.LogError("소환 위치가 충분하지 않습니다. 최소 3개의 위치가 필요합니다.");
            return;
        }

        // 복어를 총 3마리 소환합니다.
        for (int i = 0; i < 3; i++)
        {
            SpawnPufferfish(spawnPositions[i]);
        }
    }

    // 복어를 특정 위치에 소환하는 함수
    void SpawnPufferfish(Vector3 position)
    {
        if (pufferfishPrefab != null)
        {
            Instantiate(pufferfishPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Pufferfish Prefab이 설정되지 않았습니다.");
        }
    }
}

