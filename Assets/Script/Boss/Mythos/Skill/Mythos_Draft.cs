using UnityEngine;
using System.Collections;

public class Mythos_Draft : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRadius = 5f;
    public LayerMask playerLayer;

    [Header("Lift Settings")]
    public float liftHeight = 3f;
    public float liftDuration = 1f;

    [Header("Launch Settings")]
    public float horizontalForce = 50f;  // 수평 방향 힘
    public float verticalForce = 20f;    // 수직 방향 힘
    public float waitBeforeLaunch = 2f;

    [Header("Cooldown")]
    public float cooldown = 3f;
    private float nextUseTime = -1f;
    private bool isSkillActive = false;

    private Mythos mythos;

    void Start()
    {
        nextUseTime = Time.time;
        mythos = GetComponent<Mythos>();
    }

    void Update()
    {
        if (Time.time < nextUseTime || isSkillActive) return;

        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
        if (playerCollider != null && !mythos.checkAttack)
        {
            mythos.isMove = false;
            mythos.checkAttack = true;
            StartCoroutine(LiftAndLaunchPlayer(playerCollider));
        }
    }

    IEnumerator LiftAndLaunchPlayer(Collider2D player)
    {
        isSkillActive = true;
        nextUseTime = Time.time + cooldown;

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb == null)
        {
            isSkillActive = false;
            yield break;
        }

        // Player_Move, Player_Skill, Player_Attack 스크립트 비활성화
        Player_Move playerMove = player.GetComponent<Player_Move>();
        Player_Skill playerSkill = player.GetComponent<Player_Skill>();
        Player_Attack playerAttack = player.GetComponent<Player_Attack>();

        if (playerMove != null) playerMove.enabled = false;
        if (playerSkill != null) playerSkill.enabled = false;
        if (playerAttack != null) playerAttack.enabled = false;

        Vector2 startPos = player.transform.position;
        Vector2 liftPos = (Vector2)transform.position + Vector2.up * liftHeight;

        playerRb.isKinematic = true;  // 플레이어의 물리 시뮬레이션을 일시적으로 멈춤
        playerRb.linearVelocity = Vector2.zero;  // 플레이어의 초기 속도를 0으로 설정

        float elapsedTime = 0f;
        while (elapsedTime < liftDuration)
        {
            float t = elapsedTime / liftDuration;
            player.transform.position = Vector2.Lerp(startPos, liftPos, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(waitBeforeLaunch);

        playerRb.isKinematic = false;

        // 무작위로 왼쪽 또는 오른쪽 방향 선택
        float randomDirection = Random.value > 0.5f ? 1f : -1f;

        // X축과 Y축 힘을 따로 적용
        Vector2 launchForce = new Vector2(horizontalForce * randomDirection, verticalForce);
        playerRb.AddForce(launchForce, ForceMode2D.Impulse);

        // 디버그용 발사 방향 표시
        Debug.DrawLine(player.transform.position,
            (Vector2)player.transform.position + launchForce.normalized * 2,
            Color.red, 2f);

        isSkillActive = false;
        yield return new WaitForSeconds(0.5f);

        // 스킬이 끝난 후 스크립트 다시 활성화
        if (playerMove != null) playerMove.enabled = true;
        if (playerSkill != null) playerSkill.enabled = true;
        if (playerAttack != null) playerAttack.enabled = true;
        mythos.isMove = true;
        mythos.checkAttack = false;
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}