using UnityEngine;

public class Whale_of_time : MonoBehaviour
{
    public Vector3 direction = new Vector3(0, 10, 0); // �̵� ���� (x, y, z)
    public float speed = 10.0f; // �̵� �ӵ�
    float distance = 10.0f; // �̵� �Ÿ�
    public float speeddown = -0.025f; 
    public float rotationSpeed = 50f; // ȸ�� �ӵ�
    public float rotationAngle = 45f; // Z�� �ִ� ȸ�� ����

    private float currentAngle = 0f;

    private Vector3 startPos;

    void Start()
    {
        // ���� ��ġ ����
        startPos = transform.position;

        // ���� ���͸� ����ȭ (�ӵ��� ���� ������)
        //direction = direction.normalized;
        Destroy(gameObject, 5.25f);
    }

    void Update()
    {
        // PingPong�� ����� ������ �̵�
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