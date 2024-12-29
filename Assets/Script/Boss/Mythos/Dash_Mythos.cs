using UnityEngine;
using System.Collections;

public class Dash_Mythos : MonoBehaviour
{
    public float dashSpeed = 10f; // 대쉬 속도
    public float dashDuration = 0.7f; // 대쉬 지속 시간
    public float dashCooldown = 5f; // 대쉬 쿨타임
    public float detectionRadius = 3f; // 감지 반경
    public GameObject objectToSpawn;

    private float dashTime = 0f;
    public float cooldownTimer = 0f;
    public bool iscanDash = true; // 대쉬 가능 여부
    private Vector2 dashDirection = Vector2.zero; // 대쉬 방향
    private Move_Mythos moveMythosScript;
    private GameObject player;

    private bool hasSpawned = false; // 치도리 한 번만 생성하기 위한 플래그

    void Start()
    {
        // Move_Mythos 스크립트를 찾고 참조 저장
        moveMythosScript = GetComponent<Move_Mythos>();
    }

    void Update()
    {
        // player 가져옴
        player = GameObject.FindWithTag("Player");

        // 쿨타임 관리
        if (!iscanDash)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                iscanDash = true;
                moveMythosScript.isAttack = false;
                hasSpawned = false; // 쿨타임이 끝난 후, 다시 치도리를 생성할 수 있도록 초기화
            }
        }
        else
        {
            // 플레이어 감지
            DetectPlayerAndDash();
        }

        // 대쉬가 끝난 후 대쉬 동작
        if (!iscanDash && dashTime > 0)
        {
            Dash();
        }
    }

    public void Dash()
    {
        if (dashTime > 0)
        {
            // 대쉬 동작 (y축은 0으로 유지하여 x축 방향으로만 대쉬)
            transform.position += (Vector3)dashDirection * dashSpeed * Time.deltaTime;
            dashTime -= Time.deltaTime; // 대쉬 시간 감소
        }

        // 치도리 생성 한 번만 실행되도록 조건 추가
        if (!hasSpawned)
        {
            // 플레이어 위치 기준으로 대쉬 후 치도리 생성 위치 계산
            if (player.transform.position.x < this.transform.position.x)
            {
                // Player가 기준 오브젝트보다 오른쪽에 있을 경우
                hasSpawned = true;
                StartCoroutine(SpawnObject(Vector3.left, 2f));
            }
            else
            {
                // Player가 기준 오브젝트보다 왼쪽에 있을 경우
                hasSpawned = true;
                StartCoroutine(SpawnObject(Vector3.right, 2f));
            }
        }

        // 대쉬 종료
        StartDashCooldown();
    }

    void DetectPlayerAndDash()
    {
        if (iscanDash)
        {
            // 2D 감지 영역에서 플레이어 찾기
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    // 대쉬 방향 설정 (y축 값은 0으로 설정하여 x축 방향으로만 대쉬)
                    dashDirection = new Vector2(hit.transform.position.x - transform.position.x, 0).normalized;

                    // 대쉬 시작
                    moveMythosScript.isAttack = true;
                    dashTime = dashDuration; // 대쉬 시간 초기화
                    iscanDash = false; // 대쉬 비활성화
                    break;
                }
            }
        }
    }

    IEnumerator SpawnObject(Vector3 direction, float distance)
    {
        Debug.Log("치도리");
        yield return new WaitForSeconds(1f);
        Vector3 spawnPosition = this.transform.position + direction * distance;
        spawnPosition.y = -1f;
        // 오브젝트를 생성
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        moveMythosScript.isAttack = false;
    }

    void StartDashCooldown()
    {
        cooldownTimer = dashCooldown; // 쿨타임 초기화
    }

    // 감지 반경 시각화 (디버그용)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
