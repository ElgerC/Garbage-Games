using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class DoorMinigame : MonoBehaviour
{
    //very sorry about this, only way i knew how. but 5 arrays for the number of floors, with 5 intergers for their correct door
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
    public GameObject[] GO_FloorsTeleports;

    [SerializeField]
    public int I_CorrectDoorF1;

    [SerializeField]
    public int I_CorrectDoorF2;

    [SerializeField]
    public int I_CorrectDoorF3;

    [SerializeField]
    public int I_CorrectDoorF4;

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

        SetCorrectDoor();
    }

    //function for determening the random correct door and actually setting it that way
    public void SetCorrectDoor()
    {
        I_CorrectDoorF1 = Random.Range(0, GO_DoorsF1.Length);

        I_CorrectDoorF2 = Random.Range(0, GO_DoorsF2.Length);

        I_CorrectDoorF3 = Random.Range(0, GO_DoorsF3.Length);

        I_CorrectDoorF4 = Random.Range(0, GO_DoorsF4.Length);

        for (int i = 0; i < 5; i++)
        {
            if (i == I_CorrectDoorF1)
            {
                GO_DoorsF1[i].GetComponent<DoorMinigameDoors>().CorrectDoor = true;
            }

            if (i == I_CorrectDoorF2)
            {
                GO_DoorsF2[i].GetComponent<DoorMinigameDoors>().CorrectDoor = true;
            }

            if (i == I_CorrectDoorF3)
            {
                GO_DoorsF3[i].GetComponent<DoorMinigameDoors>().CorrectDoor = true;
            }

            if (i == I_CorrectDoorF4)
            {
                GO_DoorsF4[i].GetComponent<DoorMinigameDoors>().CorrectDoor = true;
            }
        }
    }
}
