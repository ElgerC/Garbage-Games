using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;

    [SerializeField] private GameObject line;


    void Update()
    {
        
    }
    private void MakeLine()
    {
        float dist = Vector3.Distance(start.transform.position, end.transform.position);
        float step = 1 / math.round(dist) * 2;

        for (int i = 1; i < math.round(dist) / 2; i++)
        {
            Vector3 loc = Vector3.Lerp(start.transform.position, end.transform.position, step * (i));
            GameObject obj = Instantiate(line, loc, Quaternion.identity, transform);
            obj.transform.LookAt(end.transform.position);
        }
    }

}
