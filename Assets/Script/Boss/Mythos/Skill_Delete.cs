using UnityEngine;
using System.Collections;
using System;

public class Skill_Delete : MonoBehaviour
{
    public float waitSecond;

    private void Start()
    {
        StartCoroutine(deleteSkill());
    }

    private IEnumerator deleteSkill()
    {
        yield return new WaitForSeconds(waitSecond);
        Destroy(gameObject);
    }
}
