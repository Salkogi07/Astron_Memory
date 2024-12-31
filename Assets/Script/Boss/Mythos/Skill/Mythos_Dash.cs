using UnityEngine;
using System.Collections;

public class Mythos_Dash : MonoBehaviour
{
    public float dashSpeed = 10f; // �뽬 �ӵ�
    public float dashDuration = 0.7f; // �뽬 ���� �ð�
    public float detectionRadius = 3f; // ���� �ݰ�
    public GameObject objectToSpawn;

    private float dashTime = 0f;
    private Vector2 dashDirection = Vector2.zero; // �뽬 ����
    private Mythos_Move mythosMove;
    private GameObject player;

    private bool hasSpawned = false; // ġ���� �� ���� �����ϱ� ���� �÷���

    void Start()
    {
        // Move_Mythos ��ũ��Ʈ�� ã�� ���� ����
        mythosMove = GetComponent<Mythos_Move>();
    }

    public void ExecuteDash()
    {
        // �÷��̾� ��ġ�� ������� �뽬 ����
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            dashDirection = new Vector2(player.transform.position.x - transform.position.x, 0).normalized;
            dashTime = dashDuration; // �뽬 �ð� �ʱ�ȭ
            mythosMove.isAttack = true;
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        while (dashTime > 0)
        {
            // �뽬 ����
            transform.position += (Vector3)dashDirection * dashSpeed * Time.deltaTime;
            dashTime -= Time.deltaTime;
            yield return null;
        }

        if (!hasSpawned)
        {
            // �÷��̾� ��ġ �������� ġ���� ���� ��ġ ���
            Vector3 spawnDirection = player.transform.position.x < transform.position.x ? Vector3.left : Vector3.right;
            StartCoroutine(SpawnObject(spawnDirection, 2f));
        }

        mythosMove.isAttack = false;
    }

    IEnumerator SpawnObject(Vector3 direction, float distance)
    {
        Debug.Log("ġ����");
        yield return new WaitForSeconds(1f);
        Vector3 spawnPosition = transform.position + direction * distance;
        spawnPosition.y = -1f;
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    // ���� �ݰ� �ð�ȭ (����׿�)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}