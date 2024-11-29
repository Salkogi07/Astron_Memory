using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    public float speed = 5f;
    public float pullSpeed = 10f;
    public int damageAmount = 10;
    public float pullOffsetY = 1f; // 플레이어를 위로 끌어당길 오프셋 Y값
    public GameObject wallcrashEffect;

    void Start()
    {
        // 2초 대기 후에 움직임 시작
        StartCoroutine(StartMovementAfterDelay(2f));
    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        // delay 시간만큼 기다림
        yield return new WaitForSeconds(delay);
        // 2초가 지난 후에 Update에서 이동 시작
        while (true)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 direction = transform.position - other.transform.position;

            // 목표 위치의 Y값을 조정해서 위쪽으로 끌어당김
            Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + pullOffsetY, transform.position.z);

            other.transform.position = Vector3.MoveTowards(other.transform.position, targetPosition, pullSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tornado_Wall"))
        {
            GameObject effect = Instantiate(wallcrashEffect,transform.position, Quaternion.identity);
            //effect.GetComponent<PlayerDamageCol>().attackDamage = damageAmount;
            Destroy(effect,0.5f);
            Destroy(gameObject);
        }
    }
}
