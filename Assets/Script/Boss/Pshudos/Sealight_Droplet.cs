using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class Sealight_Droplet : MonoBehaviour
{
    public float speed;
    public float Speed_Reduction;
    public float Duration = 2.2f;
    float Errorrange;
    public float Error_range = 10f;
    private Transform Player;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 playerPosition = Player.transform.position;
        direction = (playerPosition - (Vector2)transform.position).normalized;
        //Error_range = Random.Range(Error_range_min, Error_range_max);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // 현재 각도 계산
        Error_range = Random.Range(-Error_range, Error_range); // 오차 각도 계산
        float finalAngle = angle + Error_range; // 최종 각도

        // 최종 각도를 벡터로 변환
        float radians = finalAngle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
        speed = Random.Range(speed-5f, speed+5f);


        Destroy(gameObject, Duration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        if(speed <=0)
        {

        }
        else
        {
            speed = speed - Speed_Reduction;
        }
        
    }
}
