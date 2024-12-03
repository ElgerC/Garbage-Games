using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMashManager : MonoBehaviour
{
    [SerializeField] private List<PlayerScript> L_PlayerScripts = new List<PlayerScript>();
    Gamemanager gamemanager;
    [SerializeField] float winPresses;

    [SerializeField] private List<Vector3> L_NumberPos = new List<Vector3>();
    private void Awake()
    {
        gamemanager = Gamemanager.instance;

        for (int i = 0; i < gamemanager.players.Count; i++)
        {
            L_PlayerScripts.Add(gamemanager.players[i].GetComponent<PlayerScript>());
            L_PlayerScripts[i].V3_mashPos = L_NumberPos[i];
            L_PlayerScripts[i].G_Canvas = FindObjectOfType<Canvas>().gameObject;
        }
    }
    private void Update()
    {
        for(int i = 0;i < L_PlayerScripts.Count;i++)
        {
            if (L_PlayerScripts[i].F_Presses >= winPresses)
            {
                gamemanager.MinigameFinished(i);
            }
        }
    }
}
