using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMinigame : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> GO_Players;

    [SerializeField]
    public GameObject[] GO_DoorsF1;

    [SerializeField]
    public GameObject[] GO_DoorsF2;

    [SerializeField]
    public GameObject[] GO_DoorsF3;

    [SerializeField]
    public GameObject[] GO_DoorsF4;

    [SerializeField]
    public GameObject[] GO_DoorsF5;

    [SerializeField]
    public GameObject[] GO_FloorsTeleports;

    [SerializeField]
    public int I_CorrectDoorF1;

    [SerializeField]
    public int I_CorrectDoorF2;
    // Start is called before the first frame update
    public void Start()
    {
        for (int i = 0; i < FindObjectsOfType<PlayerScript>().Length; i++)
        {
            GO_Players.Add(FindObjectsOfType<PlayerScript>()[i].gameObject);
        }

        for (int i = 0; i < GO_Players.Count; i++)
        {
            GO_Players[i].AddComponent<DoorPlayer>();
        }
        
        I_CorrectDoorF1 = Random.Range(0, GO_DoorsF1.Length);

        I_CorrectDoorF2 = Random.Range(0, GO_DoorsF2.Length);
        
    }

    // Update is called once per frame
    public void Update()
    {

    }
}
