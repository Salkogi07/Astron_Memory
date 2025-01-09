using System;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Mythos_Roar : MonoBehaviour
{
    public GameObject Roareffect;
    private Mythos mythos;
    private Mythos_Anim anim;
    public AnimationChecker checker { get; private set; }
    void Start()
    {
        mythos = GetComponent<Mythos>();
        anim = GetComponentInChildren<Mythos_Anim>();
        checker = GetComponentInChildren<AnimationChecker>();
    }
    public void ExecuteRoar()
    {
        mythos.checkAttack = true;
        StartCoroutine(roar());
    }
    private IEnumerator roar()
    {
        // Mythos ������ ����
        if (mythos != null)
            mythos.isMove = false;
        mythos.checkAttack = false;
        anim.PlayAnimation("Mythos_roar");

        // 0.1�� ��� �� ����Ʈ ����
        yield return new WaitForSeconds(1f);
        Vector3 spawnPosition = transform.position;
        spawnPosition.z -= 1;
        Instantiate(Roareffect, spawnPosition, Quaternion.identity);

        while (!checker.IsAnimationFinished("Mythos_roar"))
        {
            yield return null; // �� ������ ���
        }
        anim.PlayAnimation("Mythos_Idle");
        yield return new WaitForSeconds(0.5f);
        // Mythos ������ �簳
        if (mythos != null)
            mythos.isMove = true;
        mythos.checkAttack = false;
    }
}