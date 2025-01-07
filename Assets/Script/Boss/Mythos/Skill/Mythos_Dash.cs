using UnityEngine;
using System.Collections;

public class Mythos_Dash : MonoBehaviour
{
    public float dashSpeed = 10f; // 대쉬 속도
    public float dashDuration = 0.7f; // 대쉬 지속 시간
    public float detectionRadius = 3f; // 감지 반경
    public GameObject objectToSpawn;

    private float dashTime = 0f;
    private Vector2 dashDirection = Vector2.zero; // 대쉬 방향
    private Mythos_Move mythosMove;
    private GameObject player;
    private Mythos mythos;
    private Mythos_Draft draft;

    private bool hasSpawned = false; // 치도리 한 번만 생성하기 위한 플래그

    void Start()
    {
        // Move_Mythos 스크립트를 찾고 참조 저장
        mythosMove = GetComponent<Mythos_Move>();
        mythos = GetComponent<Mythos>();
        draft = GetComponent<Mythos_Draft>();
    }

    public void ExecuteDash()
    {
        // 플레이어 위치를 기반으로 대쉬 실행
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            dashDirection = new Vector2(player.transform.position.x - transform.position.x, 0).normalized;
            dashTime = dashDuration; // 대쉬 시간 초기화
            mythosMove.isAttack = true;
            mythos.checkAttack = true;
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        while (dashTime > 0)
        {
            // 대쉬 동작
            transform.position += (Vector3)dashDirection * dashSpeed * Time.deltaTime;
            dashTime -= Time.deltaTime;
            yield return null;
        }

        if (!hasSpawned)
        {
            // 플레이어 위치 기준으로 치도리 생성 위치 계산
            Vector3 spawnDirection = player.transform.position.x < transform.position.x ? Vector3.left : Vector3.right;
            mythos.isMove = false;
            StartCoroutine(SpawnObject(spawnDirection, 0.5f));
        }

        mythosMove.isAttack = false;
        mythos.checkAttack = false;
    }

    IEnumerator SpawnObject(Vector3 direction, float distance)
    {
        Debug.Log("치도리");
        yield return new WaitForSeconds(1f);
        Vector3 spawnPosition = transform.position + direction * distance;
        spawnPosition.y = -1f;
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        mythos.isMove = true;
    }

    // 감지 반경 시각화 (디버그용)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
