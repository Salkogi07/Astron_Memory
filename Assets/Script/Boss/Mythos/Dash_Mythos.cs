using UnityEngine;
using System.Collections;

public class Dash_Mythos : MonoBehaviour
{
    public float dashSpeed = 10f; // �뽬 �ӵ�
    public float dashDuration = 0.7f; // �뽬 ���� �ð�
    public float dashCooldown = 5f; // �뽬 ��Ÿ��
    public float detectionRadius = 3f; // ���� �ݰ�
    public GameObject objectToSpawn;

    private float dashTime = 0f;
    public float cooldownTimer = 0f;
    public bool iscanDash = true; // �뽬 ���� ����
    private Vector2 dashDirection = Vector2.zero; // �뽬 ����
    private Move_Mythos moveMythosScript;
    private GameObject player;

    private bool hasSpawned = false; // ġ���� �� ���� �����ϱ� ���� �÷���

    void Start()
    {
        // Move_Mythos ��ũ��Ʈ�� ã�� ���� ����
        moveMythosScript = GetComponent<Move_Mythos>();
    }

    void Update()
    {
        // player ������
        player = GameObject.FindWithTag("Player");

        // ��Ÿ�� ����
        if (!iscanDash)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                iscanDash = true;
                moveMythosScript.isAttack = false;
                hasSpawned = false; // ��Ÿ���� ���� ��, �ٽ� ġ������ ������ �� �ֵ��� �ʱ�ȭ
            }
        }
        else
        {
            // �÷��̾� ����
            DetectPlayerAndDash();
        }

        // �뽬�� ���� �� �뽬 ����
        if (!iscanDash && dashTime > 0)
        {
            Dash();
        }
    }

    public void Dash()
    {
        if (dashTime > 0)
        {
            // �뽬 ���� (y���� 0���� �����Ͽ� x�� �������θ� �뽬)
            transform.position += (Vector3)dashDirection * dashSpeed * Time.deltaTime;
            dashTime -= Time.deltaTime; // �뽬 �ð� ����
        }

        // ġ���� ���� �� ���� ����ǵ��� ���� �߰�
        if (!hasSpawned)
        {
            // �÷��̾� ��ġ �������� �뽬 �� ġ���� ���� ��ġ ���
            if (player.transform.position.x < this.transform.position.x)
            {
                // Player�� ���� ������Ʈ���� �����ʿ� ���� ���
                hasSpawned = true;
                StartCoroutine(SpawnObject(Vector3.left, 2f));
            }
            else
            {
                // Player�� ���� ������Ʈ���� ���ʿ� ���� ���
                hasSpawned = true;
                StartCoroutine(SpawnObject(Vector3.right, 2f));
            }
        }

        // �뽬 ����
        StartDashCooldown();
    }

    void DetectPlayerAndDash()
    {
        if (iscanDash)
        {
            // 2D ���� �������� �÷��̾� ã��
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

            foreach (var hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    // �뽬 ���� ���� (y�� ���� 0���� �����Ͽ� x�� �������θ� �뽬)
                    dashDirection = new Vector2(hit.transform.position.x - transform.position.x, 0).normalized;

                    // �뽬 ����
                    moveMythosScript.isAttack = true;
                    dashTime = dashDuration; // �뽬 �ð� �ʱ�ȭ
                    iscanDash = false; // �뽬 ��Ȱ��ȭ
                    break;
                }
            }
        }
    }

    IEnumerator SpawnObject(Vector3 direction, float distance)
    {
        Debug.Log("ġ����");
        yield return new WaitForSeconds(1f);
        Vector3 spawnPosition = this.transform.position + direction * distance;
        spawnPosition.y = -1f;
        // ������Ʈ�� ����
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
        moveMythosScript.isAttack = false;
    }

    void StartDashCooldown()
    {
        cooldownTimer = dashCooldown; // ��Ÿ�� �ʱ�ȭ
    }

    // ���� �ݰ� �ð�ȭ (����׿�)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
