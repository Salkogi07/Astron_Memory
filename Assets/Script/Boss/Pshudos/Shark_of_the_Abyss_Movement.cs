using UnityEngine;

public class Shark_of_the_Abyss_Movement : MonoBehaviour
{
    public float speed;
    float time;
    public float delay;
    private Transform Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = delay;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 playerPosition = Player.transform.position;
        Vector2 direction = (playerPosition - (Vector2)transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        time -= Time.deltaTime;
        if (time <= 0)
        {
            dash();
            time = delay;
        }
    }
    void dash()
    {

    }
}
