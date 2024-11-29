using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

enum golfStates
{
    Starting,
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
    private int I_ballIndex = 0;

    [SerializeField] private golfStates state = golfStates.Starting;

    [SerializeField] private Vector3 V3_offSet;

    [SerializeField] private GameObject G_goal;

    public static MinigolfManager instance;

    [SerializeField] private GameObject temp;

    [SerializeField] private GameObject G_line;

    //Camera variables
    [SerializeField] private Animator ANIM_CamAnim;
    [SerializeField] private GameObject G_Cam;

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
        G_curBall.GetComponent<MeshRenderer>().material.color = Gamemanager.instance.colorList[I_ballIndex];

        BS_curBallScript = G_curBall.GetComponent<BallScript>();
        RB_curBallRb = G_curBall.GetComponent<Rigidbody>();
        Gamemanager.instance.players[I_ballIndex].GetComponent<PlayerScript>().G_golfBall = G_curBall;

        I_ballIndex++;
        L_balls.Add(G_curBall);

        state = golfStates.Busy;
    }
    public void FinishedStartAnim()
    {
        ANIM_CamAnim.enabled = false;
        state = golfStates.Waiting;
    }

    private void Update()
    {
        switch (state)
        {
            case golfStates.Waiting:
                SpawnBall();
                break;
            case golfStates.Busy:
                if (!BS_curBallScript.B_Lauched)
                {
                    Camera.main.transform.position = G_curBall.transform.position + V3_offSet;
                    Camera.main.transform.rotation = Quaternion.Euler(14.5f, 0, 0);
                }
                else
                {
                    Camera.main.transform.position = new Vector3(1.17f, 12.2f, -15.9f);
                    Camera.main.transform.LookAt(G_curBall.transform.position);
                }
                    

                if (BS_curBallScript.B_landed)
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
                ANIM_CamAnim.enabled = true;
                ANIM_CamAnim.SetTrigger("End");
                MakeLine();
                break;
        }
    }
    public void CheckWinner()
    {
        ANIM_CamAnim.enabled = false;

        float dist = 0;
        int winner = 8;
        for (int i = 0; i < L_balls.Count; i++)
        {
            if (Vector3.Distance(L_balls[i].transform.position, G_goal.transform.position) > dist)
            {
                dist = Vector3.Distance(L_balls[i].transform.position, G_goal.transform.position);
                winner = i;
            }
        }

        Camera.main.transform.position = Gamemanager.instance.players[winner].GetComponent<PlayerScript>().G_golfBall.transform.position + V3_offSet;
        Camera.main.transform.rotation = Quaternion.Euler(14.5f, 0, 0);

        temp.SetActive(true);
        StartCoroutine(wait(winner));

        state = golfStates.Done;
    }
    IEnumerator wait(int winner)
    {
        yield return new WaitForSeconds(4);
        Gamemanager.instance.MinigameFinished(winner);
    }
    private void MakeLine()
    {
        GameObject G_end = G_goal;
        for (int j = 0; j < L_balls.Count; j++)
        {
            GameObject G_start = L_balls[j];
            Material M_lineMat = L_balls[j].GetComponent<MeshRenderer>().material;

            float dist = Vector3.Distance(G_start.transform.position, G_end.transform.position);
            float step = 1 / math.round(dist) * 2;

            for (int i = 1; i < math.round(dist) / 2; i++)
            {
                Vector3 loc = Vector3.Lerp(G_start.transform.position, G_end.transform.position, step * (i));
                GameObject obj = Instantiate(G_line, loc, Quaternion.identity, transform);
                obj.transform.LookAt(G_end.transform.position);
                obj.GetComponent<MeshRenderer>().material.color = M_lineMat.color;
            }
        }
        
    }
}
