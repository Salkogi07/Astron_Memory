using UnityEngine;
using System.Collections;

public class Blow_String : MonoBehaviour
{
    public float speed = 10f;
    public float Duration = 1.2f;
    private Transform Player;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 playerPosition = Player.transform.position;
        direction = (playerPosition - (Vector2)transform.position).normalized;

        //Vector2 direction = (Player.position - transform.position).normalized;

        //StartCoroutine(s());
        //transform.position +=
        
        Destroy(gameObject, Duration);
    }


    void shoot()
    {        

    }
    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        //transform.position += direction * speed *  Time.deltaTime;
        //rb.velocity = direction * projectileSpeed;

        //Rigidbody2D rb = projectileScript.GetComponent<Rigidbody2D>();




        // 플레이어를 향해 직선 이동
        //Vector3 direction = (target.position - transform.position).normalized;

    }
    //IEnumerator s()
    //{
       
    //}

    void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌 시 동작
        if (other.CompareTag("Player"))
        {
            Debug.Log("Projectile hit the player!");
            Destroy(gameObject); // 투사체 파괴
        }
    }
}
