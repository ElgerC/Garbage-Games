using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Club : MonoBehaviour
{
    Animator anim_clubAnimator;
    void Awake()
    {
        anim_clubAnimator = GetComponent<Animator>();
    }
    public IEnumerator swing()
    {
        Vector3 V3_goal = new Vector3(-transform.rotation.x,0,0);
        Vector3 V3_start = new Vector3(transform.rotation.x,0,0);

        for (int i = 0; i < 60; i++)
        {
            transform.rotation = quaternion.Euler(Vector3.Lerp(V3_start, V3_goal,0.016f));
            yield return new WaitForSeconds(0.01f);
        }
    }
}
