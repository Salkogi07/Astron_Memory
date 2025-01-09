
using System.Collections.Generic;
using UnityEngine;

public class of_time : MonoBehaviour
{
    [SerializeField]
    
    public List<int> randomnumber = new List<int> { 0,90,180,270};
    int targetAngle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        targetAngle = Random.Range(0,randomnumber.Count);
        transform.eulerAngles = new Vector3(0, 0, randomnumber[targetAngle]);
        transform.position = new Vector3(45, 0, 0);
        Destroy(gameObject,1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
