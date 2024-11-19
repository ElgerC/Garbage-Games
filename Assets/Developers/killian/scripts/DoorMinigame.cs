using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMinigame : MonoBehaviour
{
    [SerializeField]
    private GameObject[] GO_Doors;

    [SerializeField]
    private GameObject GO_DoorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < GO_Doors.Length; i++)
        {
            Instantiate(GO_DoorPrefab);
        }
    }
}
