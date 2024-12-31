using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Mythos : MonoBehaviour
{
    public AnimationChecker checker { get; private set; }
    private Mythos_Dash dash;
    private Mythos_Anim anim;

    private List<int> bag = new List<int>();

    public float skill1_WaitTime = 1;
    public float Skill2_WaitTime = 1;
    public float Skill3_WaitTime = 1;

    public bool isMove = false;

    private void Awake()
    {
        dash = GetComponent<Mythos_Dash>();
        checker = GetComponentInChildren<AnimationChecker>();
        anim = GetComponentInChildren<Mythos_Anim>();
    }

    private void Start()
    {
        StartCoroutine(Appeared());
    }

    IEnumerator Appeared()
    {
        isMove = false;
        anim.PlayAnimation("Mythos_Appeared");
        while (!checker.IsAnimationFinished("Mythos_Appeared"))
        {
            yield return null; // �� ������ ���
        }
        anim.PlayAnimation("Mythos_Idle");
        StartCoroutine(ChooseSkill());
        isMove = true;
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
                    yield return new WaitForSeconds(skill1_WaitTime);
                    Debug.Log("0�� ������ �����");
                    break;
                case 1:
                    yield return new WaitForSeconds(Skill2_WaitTime);
                    Debug.Log("1�� ������ �����");
                    dash.ExecuteDash();
                    break;
                case 2:
                    yield return new WaitForSeconds(Skill3_WaitTime);
                    Debug.Log("2�� ������ �����");
                    break;
            }
        }
    }

    int GetFromBag()
    {
        if (bag.Count == 0)
            bag = new List<int>() { 0, 1, 2 };

        int rng = Random.Range(0, bag.Count);
        int result = bag[rng];
        bag.RemoveAt(rng);

        return result;
    }
}
