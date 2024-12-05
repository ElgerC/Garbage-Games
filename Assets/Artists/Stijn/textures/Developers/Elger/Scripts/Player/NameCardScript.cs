using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameCardScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> crowns = new List<GameObject>();
    private void Start()
    {
        for (int i = 0; i < crowns.Count; i++)
        {
            crowns[i].SetActive(false);
        }
    }
    public void PlaceCrowns(int amount)
    {
        if (amount < 4)
            for (int i = 0; i < amount; i++)
            {
                crowns[i].SetActive(true);
            }
    }
}
