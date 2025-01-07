using UnityEngine;

public class Pshudos_Whale_of_Time : MonoBehaviour
{
    public GameObject Whale_of_Time;
    public GameObject of_Time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Whale_of_time()
    {
        GameObject Whale_of_time = Instantiate(Whale_of_Time, transform.position, Quaternion.identity);
        GameObject of_time = Instantiate(Whale_of_Time, transform.position, Quaternion.identity);
    }
}

    
