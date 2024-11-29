using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado_BossController : MonoBehaviour
{
    private Tornado_BossSkill skill;
    private Transform player;


    public bool isBossSkill = false;
    public bool isBossSkill_Ready = false;
    public float speed = 2f;

    public float Tornado_Fire_WaitTime = 1;
    public float Rain_Fire_WaitTime = 1;
    public float Rock_Fire_WaitTime = 1;

    public bool isDash = false;

    private List<int> bag = new List<int>();

    private void Awake()
    {
        skill = GetComponent<Tornado_BossSkill>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    int GetFromBag()
    {
        if (bag.Count == 0)
            bag = new List<int>() { 0, 1, 2};

        int rng = Random.Range(0, bag.Count);
        int result = bag[rng];
        bag.RemoveAt(rng);

        return result;
    }

    void Start()
    {
        StartCoroutine(ChooseSkill());
    }

    IEnumerator ChooseSkill()
    {
        while (true)
        {
            int randomDelay = Random.Range(4, 7);
            yield return new WaitForSeconds(randomDelay);
            switch (GetFromBag())
            {
                case 0:
                    isBossSkill_Ready = true;
                    yield return new WaitForSeconds(Tornado_Fire_WaitTime);
                    isBossSkill_Ready = false;
                    Debug.Log("0번 패턴이 실행됨");
                    skill.Tornado_Fire_Start();
                    break;
                case 1:
                    isBossSkill_Ready = true;
                    yield return new WaitForSeconds(Rain_Fire_WaitTime);
                    isBossSkill_Ready = false;
                    Debug.Log("1번 패턴이 실행됨");
                    skill.Rain_Fire_Start();
                    break;
                case 2:
                    isBossSkill_Ready = true;
                    yield return new WaitForSeconds(Rock_Fire_WaitTime);
                    isBossSkill_Ready = false;
                    Debug.Log("2번 패턴이 실행됨");
                    skill.Rock_Fire_Start();
                    break;
            }
        }
    }

    void Update()
    {
        if(isBossSkill || isBossSkill_Ready)
            return;

        if (player != null && !isDash)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();

            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

    }
}
