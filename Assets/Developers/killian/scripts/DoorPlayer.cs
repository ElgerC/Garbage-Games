using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
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

    // actual collision check
    public void OnCollisionEnter(Collision collision)
    {
        FloorTeleport(collision.gameObject);
    }

    //not sure if needed, but scared to remove incase it breaks everything
    public void FloorTeleport(GameObject CorrectDoor)
    {
        StartCoroutine(NextFloor(CorrectDoor));
    }

    //teleports the player when the door is correct to the nexxt floor, switch statement checks the current from on the character and teleports to them to the correct floor.
    public IEnumerator NextFloor(GameObject Door)
    {
        yield return new WaitForSeconds(0.1f);

        if( Door.GetComponent<DoorMinigameDoors>().CorrectDoor == true)
        {
            switch (CurrentFloor)
            {
                case 5:
                    transform.position = DoorMinigame.GO_FloorsTeleports[0].transform.position;
                    CurrentFloor--;
                    break;
                case 4:
                    transform.position = DoorMinigame.GO_FloorsTeleports[1].transform.position;
                    CurrentFloor--;
                    break;
                case 3:
                    transform.position = DoorMinigame.GO_FloorsTeleports[2].transform.position;
                    CurrentFloor--;
                    break;
                case 2:
                    transform.position = DoorMinigame.GO_FloorsTeleports[3].transform.position;
                    CurrentFloor--;
                    break;
                case 1:
                    transform.position = DoorMinigame.GO_FloorsTeleports[4].transform.position;
                    CurrentFloor--;
                    break;
            }
        }
    }
}
