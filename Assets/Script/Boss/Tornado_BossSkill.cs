using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado_BossSkill : MonoBehaviour
{
    [Header("Componet")]
    Tornado_BossController controller;

    [Header("Tornado_Fire info")]
    public GameObject tornadoSmall_Prefab;
    public Transform[] tornadoSmall_Point;

    [Header("Rain_Fire info")]
    public GameObject rainProjectilePrefab;
    public GameObject dropletPrefab;
    public GameObject explosionPrefab;
    public int numberOfDroplets = 8;

    private void Awake()
    {
        controller = GetComponent<Tornado_BossController>();
    }

    public void Tornado_Fire_Start()
    {
        StartCoroutine(Tornado_Fire());
    }

    public void Rain_Fire_Start()
    {
        StartCoroutine(Rain_Fire());
    }

    public void Rock_Fire_Start()
    {
        StartCoroutine(Rock_Fire());
    }

    IEnumerator Tornado_Fire()
    {
        controller.isBossSkill = true;

        GameObject tornado1 = Instantiate(tornadoSmall_Prefab, tornadoSmall_Point[0].transform.position, Quaternion.identity);
        GameObject tornado2 = Instantiate(tornadoSmall_Prefab, tornadoSmall_Point[1].transform.position, Quaternion.Euler(0, 180, 0));

        Destroy(tornado1, 7);
        Destroy(tornado2, 7);

        yield return new WaitForSeconds(7);

        controller.isBossSkill = false;
    }

    IEnumerator Rain_Fire()
    {
        controller.isBossSkill = true;

        GameObject rainProjectile = Instantiate(rainProjectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rainRb = rainProjectile.GetComponent<Rigidbody2D>();

        float elapsedTime = 0f;
        float duration = 1f; // �� ������ �ö󰡴� �ð� (1�ʷ� ����)
        float maxDistance = 8f; // �߻�ü�� ������ �ִ� ����

        // �ʱ� �ӵ��� 0���� ����
        rainRb.linearVelocity = Vector3.up * 0f;

        // õõ�� �ö󰡸鼭 ���� �����ϴ� ����
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float speed = Mathf.Lerp(0f, 30f, elapsedTime / duration); // ���� ������ ���� (�ӵ� �ִ� 30)
            rainRb.linearVelocity = Vector3.up * speed;

            // ������ �߻�ü ���� �Ÿ��� ���
            float distanceToBoss = Vector3.Distance(rainProjectile.transform.position, transform.position);

            // �������� �Ÿ��� maxDistance �̻��̸� ���߱�
            if (distanceToBoss >= maxDistance)
            {
                rainRb.linearVelocity = Vector3.zero; // �ӵ��� 0���� �����Ͽ� ����
                break;
            }

            yield return null; // ���� �����ӱ��� ���
        }

        // �߻�ü�� �ִ� ���̿� �����ϸ� ũ�� ���� �� ������ ȿ��
        float explosionTime = 0f;
        float maxExplosionTime = 3f; // 1�� ���� ũ�� ����

        Vector3 originalScale = rainProjectile.transform.localScale;
        Vector3 targetScale = originalScale * 5f; // �ִ� 3����� Ŀ���� ����

        while (explosionTime < maxExplosionTime)
        {
            explosionTime += Time.deltaTime;
            rainProjectile.transform.localScale = Vector3.Lerp(originalScale, targetScale, explosionTime / maxExplosionTime);
            yield return null; // ���� �����ӱ��� ���
        }

        // ������ ȿ�� ����
        GameObject explosion = Instantiate(explosionPrefab, rainProjectile.transform.position, Quaternion.identity); // ���� ����Ʈ ����
        Destroy(explosion, 0.5f);
        Destroy(rainProjectile); // �߻�ü ����

        // ���� ������� ���� (���� ��)
        for (int i = 0; i < numberOfDroplets; i++)
        {
            float angle = i * (360f / numberOfDroplets);
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            Vector3 direction = rotation * Vector3.forward;

            Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
            GameObject droplet = Instantiate(dropletPrefab, rainProjectile.transform.position + randomOffset, Quaternion.identity);
            Rigidbody2D dropletRb = droplet.GetComponent<Rigidbody2D>();
            dropletRb.linearVelocity = direction * 5f;

            //droplet�̻��� ����°� �����ߵ�
        }

        controller.isBossSkill = false;
    }




    IEnumerator Rock_Fire()
    {
        controller.isBossSkill = true;
        yield return new WaitForSeconds(1);
        controller.isBossSkill = false;
    }
}
