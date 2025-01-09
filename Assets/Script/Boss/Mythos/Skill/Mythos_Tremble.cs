using UnityEngine;
using System.Collections;

public class Mythos_Tremble : MonoBehaviour
{
    public GameObject objectToSpawn;  // 생성할 오브젝트 프리팹
    public float spawnY = 2f;        // Y 좌표 기준
    public float spawnInterval = 0.1f; // 오브젝트 생성 간격
    private Mythos mythos;           // Mythos 스크립트 참조

    void Start()
    {
        mythos = GetComponent<Mythos>(); // Mythos 스크립트 가져오기
    }

    public void ExecuteTremble()
    {
        mythos.checkAttack = true;
        StartCoroutine(TrembleRoutine());
    }

    private IEnumerator TrembleRoutine()
    {
        // Mythos 움직임 정지
        if (mythos != null)
            mythos.isMove = false;

        // 플레이어 방향 결정
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            yield break;
        }

        float direction = player.transform.position.x > transform.position.x ? 1f : -1f;

        // 1초 대기 (isMove 정지 상태 유지)
        yield return new WaitForSeconds(1f);

        // 오브젝트 생성
        Vector3 startPosition = new Vector3(transform.position.x, spawnY, 0);
        SpawnObject(startPosition, direction, 0);
        yield return new WaitForSeconds(spawnInterval);
        SpawnObject(startPosition + Vector3.right * 2 * direction, direction, 1);
        yield return new WaitForSeconds(spawnInterval);
        SpawnObject(startPosition + Vector3.right * 4 * direction, direction, 2);
        yield return new WaitForSeconds(spawnInterval);
        SpawnObject(startPosition + Vector3.right * 6 * direction, direction, 3);
        yield return new WaitForSeconds(spawnInterval);
        SpawnObject(startPosition + Vector3.right * 8    * direction, direction, 4);

        yield return new WaitForSeconds(0.5f);

        // Mythos 움직임 재개
        if (mythos != null)
            mythos.isMove = true;
        mythos.checkAttack = false;
    }

    private void SpawnObject(Vector3 position, float direction, int index)
    {
        Instantiate(objectToSpawn, position, Quaternion.identity);
    }
}
