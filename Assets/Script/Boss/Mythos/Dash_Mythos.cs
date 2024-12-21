using UnityEngine;

public class Dash_Mythos : MonoBehaviour
{
    public float dashSpeed = 10f; // �뽬 �ӵ�
    public float dashDuration = 0.7f; // �뽬 ���� �ð�
    public float dashCooldown = 5f; // �뽬 ��Ÿ��
    public float detectionRadius = 3f; // ���� �ݰ�

    private float dashTime = 0f;
    public float cooldownTimer = 0f;
    public bool iscanDash = true; // �뽬 ���� ����
    private Vector2 dashDirection = Vector2.zero; // �뽬 ����
    private Move_Mythos moveMythosScript;

    void Start()
    {
        // Move_Mythos ��ũ��Ʈ�� ã�� ���� ����
        moveMythosScript = GetComponent<Move_Mythos>();
    }

    void Update()
    {
        // ��Ÿ�� ����
        if (!iscanDash)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                iscanDash = true;
                moveMythosScript.isAttack = false;
            }
        }
        else
        {
            // �÷��̾� ����
            DetectPlayerAndDash();
        }

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
        // �뽬 ����
        StartDashCooldown();
        moveMythosScript.isAttack = true; // �÷��̾��� �������� ����
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
    