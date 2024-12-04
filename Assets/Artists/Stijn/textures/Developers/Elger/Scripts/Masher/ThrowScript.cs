using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> garbage = new List<GameObject>();

    private void Start()
    {
        GameObject go = Instantiate(garbage[Random.Range(0,garbage.Count-1)],gameObject.transform.GetChild(0).transform);
        go.transform.localPosition = Vector3.zero;
    }
    public void Deliverd()
    {
        Destroy(gameObject);
    }
}
