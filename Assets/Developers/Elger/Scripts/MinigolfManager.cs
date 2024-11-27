using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum golfStates
{
    Waiting,
    Busy,
    Checking,
    Done
}
public class MinigolfManager : MonoBehaviour
{
    [SerializeField] private GameObject G_ballPrefab;
    private List<GameObject> L_balls = new List<GameObject>();
    [SerializeField] private Vector3 V3_startPos;

    private GameObject G_curBall;
    private BallScript BS_curBallScript;
    private Rigidbody RB_curBallRb;

    [SerializeField] private golfStates state = golfStates.Waiting;

    private int I_ballIndex = 0;

    [SerializeField] private GameObject G_goal;

    public static MinigolfManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(this);
    }
    private void SpawnBall()
    {
        G_curBall = Instantiate(G_ballPrefab, V3_startPos, Quaternion.Euler(0, 180, 0));

        BS_curBallScript = G_curBall.GetComponent<BallScript>();
        RB_curBallRb = G_curBall.GetComponent<Rigidbody>();
        Gamemanager.instance.players[I_ballIndex].GetComponent<PlayerScript>().G_golfBall = G_curBall;

        I_ballIndex++;
        L_balls.Add(G_curBall);

        state = golfStates.Busy;
    }
    private void CheckWinner()
    {
        float dist = 0;
        int winner = 8;
        for (int i = 0; i < L_balls.Count; i++)
        {
            if (Vector3.Distance(L_balls[i].transform.position,G_goal.transform.position) > dist)
            {
                dist = Vector3.Distance(L_balls[i].transform.position, G_goal.transform.position);
                winner = i;
            }
        }

        Gamemanager.instance.MinigameFinished(winner);

        state = golfStates.Done;
    }
    private void Update()
    {
        switch (state)
        {
            case golfStates.Waiting:
                SpawnBall();
                
                break;
            case golfStates.Busy:
                Debug.Log(BS_curBallScript.B_landed);
                if (BS_curBallScript.B_landed && RB_curBallRb.velocity.magnitude == 0)
                {
                    if (I_ballIndex == Gamemanager.instance.players.Count)
                    {
                        state = golfStates.Checking;
                    }
                    else
                    {
                        state = golfStates.Waiting;
                    }
                }

                break;
            case golfStates.Checking:
                CheckWinner();
                break;
        }
    }

}
