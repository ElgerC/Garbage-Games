using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DoorPlayer : MonoBehaviour
{
    public DoorMinigame DoorMinigame;

    [SerializeField]
    public int CurrentFloor = 4;

    Gamemanager gamemanager;

    private void Awake()
    {
        gamemanager = Gamemanager.instance;
    }
    // Start is called before the first frame update
    public void Start()
    {
        DoorMinigame = FindObjectOfType<DoorMinigame>();
    }

    // actual collision check
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("DoorMinigame"))
        {
            transform.position = DoorMinigame.AwaitingTeleport.transform.position;
            FloorTeleport(collision.gameObject);
        }

    }

    //not sure if needed, but scared to remove incase it breaks everything
    public void FloorTeleport(GameObject CorrectDoor)
    {
        StartCoroutine(NextFloor(CorrectDoor));
    }

    //teleports the player when the door is correct to the nexxt floor, switch statement checks the current from on the character and teleports to them to the correct floor.
    public IEnumerator NextFloor(GameObject Door)
    {
        Door.GetComponentInParent<Animator>().SetTrigger("Open");
        yield return new WaitForSeconds(2f);


        if ( Door.GetComponent<DoorMinigameDoors>().CorrectDoor == true)
        {
            switch (CurrentFloor)
            {
                case 4:
                    transform.position = DoorMinigame.GO_FloorsTeleports[0].transform.position;

                    CurrentFloor--;

                    break;
                case 3:
                    transform.position = DoorMinigame.GO_FloorsTeleports[1].transform.position;

                    CurrentFloor--;

                    break;
                case 2:
                    transform.position = DoorMinigame.GO_FloorsTeleports[2].transform.position;

                    CurrentFloor--;

                    break;
                case 1:
                    transform.position = DoorMinigame.GO_FloorsTeleports[3].transform.position;

                    CurrentFloor--;

                    Gamemanager.instance.MinigameFinished(FindWinner());

                    break;
            }
        }else if(Door.GetComponent<DoorMinigameDoors>().CorrectDoor != true)
        {
            switch (CurrentFloor)
            {
                case 4:
                    transform.position = DoorMinigame.GO_WrongDoorsTeleport[0].transform.position;
                    break;
                case 3:
                    transform.position = DoorMinigame.GO_WrongDoorsTeleport[1].transform.position;
                    break;
                case 2: 
                    transform.position = DoorMinigame.GO_WrongDoorsTeleport[2].transform.position;
                    break;
                case 1: 
                    transform.position = DoorMinigame.GO_WrongDoorsTeleport[3].transform.position;
                    break;
            }
        }
    }
    //Made by Elger
    private int FindWinner()
    {
        for (int i = 0; i < gamemanager.players.Count; i++)
        {
            GameObject G_obj = gamemanager.players[i];
            if (G_obj.tag == transform.tag)
            {
                return i;
            }
        }
        return 0;
    }
}
