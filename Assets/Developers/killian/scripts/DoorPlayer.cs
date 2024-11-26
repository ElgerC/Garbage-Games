using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayer : MonoBehaviour
{
    public DoorMinigame DoorMinigame;

    [SerializeField]
    public int CurrentFloor = 5;
    // Start is called before the first frame update
    public void Start()
    {
        DoorMinigame = FindObjectOfType<DoorMinigame>();
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        TeleportPlayer();
    }

    public void TeleportPlayer()
    {
        StartCoroutine(NextFloor());
    }

    public IEnumerator NextFloor()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < DoorMinigame.GO_DoorsF1.Length; i++)
        {
            if ( i == DoorMinigame.I_CorrectDoorF1)
            {
                transform.position = DoorMinigame.GO_FloorsTeleports[0].transform.position;
                CurrentFloor--;
            }
        }
    }
}
