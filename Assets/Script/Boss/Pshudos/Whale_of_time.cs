using UnityEngine;

public class Whale_of_time : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 10, 0); // 이동 방향 (x, y, z)
    public float speed = 10.0f; // 이동 속도
    float distance = 10.0f; // 이동 거리
    public float speeddown = -0.025f; 
    public float rotationSpeed = 50f; // 회전 속도
    public float rotationAngle = 45f; // Z축 최대 회전 각도

    private float currentAngle = 0f;

    private Vector3 startPos;

    void Start()
    {
        // 시작 위치 저장
        startPos = transform.position;

        // 방향 벡터를 정규화 (속도에 영향 없도록)
        //direction = direction.normalized;
        Destroy(gameObject, 5.25f);
    }

    void Update()
    {
        // PingPong을 사용해 방향대로 이동
        //float moveAmount = Mathf.PingPong(Time.time * speed);
        //transform.position = startPos + direction * Time.time *speed;
        transform.position = new Vector2(13 ,Time.time * speed);
        //currentAngle = Mathf.PingPong(Time.time * rotationSpeed, rotationAngle * 2) - rotationAngle;

        speed = speed + speeddown * Time.time;
        Debug.Log($"{speed}");
        if(speed <=1f)
        {
        currentAngle = currentAngle - 0.1f * Time.time;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        }
    }
}