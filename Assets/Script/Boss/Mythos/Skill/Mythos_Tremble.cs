using UnityEngine;
using System.Collections;

public class Mythos_Tremble : MonoBehaviour
{
    public GameObject objectToSpawn;  // ������ ������Ʈ ������
    public float spawnY = 2f;        // Y ��ǥ ����
    public float spawnInterval = 0.1f; // ������Ʈ ���� ����
    private Mythos mythos;           // Mythos ��ũ��Ʈ ����

    void Start()
    {
        mythos = GetComponent<Mythos>(); // Mythos ��ũ��Ʈ ��������
    }

    public void ExecuteTremble()
    {
        mythos.checkAttack = true;
        StartCoroutine(TrembleRoutine());
    }

    private IEnumerator TrembleRoutine()
    {
        // Mythos ������ ����
        if (mythos != null)
            mythos.isMove = false;

        // �÷��̾� ���� ����
        GameObject player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            yield break;
        }

        float direction = player.transform.position.x > transform.position.x ? 1f : -1f;

        // 1�� ��� (isMove ���� ���� ����)
        yield return new WaitForSeconds(1f);

        // ������Ʈ ����
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

        // Mythos ������ �簳
        if (mythos != null)
            mythos.isMove = true;
        mythos.checkAttack = false;
    }

    private void SpawnObject(Vector3 position, float direction, int index)
    {
        Instantiate(objectToSpawn, position, Quaternion.identity);
    }
}
