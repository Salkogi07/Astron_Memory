using UnityEngine;


public class pshudos_blowfish : MonoBehaviour
{
    private Transform Player;
    public GameObject sting;

    public float delay = 2.5f;
    public float time = 2.5f;

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0 )
        {
            FireProjectile();
            time = delay;
        }
        
    }
    void FireProjectile()
    {
        GameObject Blow_String = Instantiate(sting, transform.position, Quaternion.identity);
        Blow_String projectileScript = Blow_String.GetComponent<Blow_String>();
        //Rigidbody2D rb = projectileScript.GetComponent<Rigidbody2D>();
        //if (projectileScript != null)
        //{
        //    projectileScript.target = Player; // 플레이어를 목표로 설정
        //}
    }
    
}
