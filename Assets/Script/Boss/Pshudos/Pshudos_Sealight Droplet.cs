using UnityEngine;

public class SealightDroplet : MonoBehaviour
{
    public float Cooldown = 5f;
    float time = 5f;
    int number;
    public int minnumber = 12;
    public int maxnumber = 18;
    public GameObject Sealight_water_Droplet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        //Debug.Log(time);
        if (time <= 0f)
        {
            SealightDroplet_shoot();
            time = Cooldown;
        }
    }

    void SealightDroplet_shoot()
    {
        Debug.Log("¾ÆÀÕ");
        number = Random.Range(minnumber, maxnumber);
        Debug.Log($"»ý¼ºÇÒ ¹°¹æ¿ï °³¼ö: {number}");
        for (int i=0; i< number; i++)
        {
            GameObject Sealight_Droplet = Instantiate(Sealight_water_Droplet, transform.position, Quaternion.identity);
            Sealight_Droplet projectileScript = Sealight_Droplet.GetComponent<Sealight_Droplet>();
            //Debug.Log("¹Ù´åºû ¹°¹æ¿ï");
        }
    }
}
