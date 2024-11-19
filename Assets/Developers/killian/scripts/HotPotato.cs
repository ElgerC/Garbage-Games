using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

public class HotPotato : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> GO_Players;

    [SerializeField]
    private GameObject GO_Bombprefab;
    [SerializeField]
    public int I_RandomPlayer;
    [SerializeField]
    private int I_PlayersLeft = 4;

    [SerializeField]
    private float F_BombTimer = 10;

    private Vector3 V3_Offset = new Vector3(0, 2, 0);
    // Start is called before the first frame update
    void Start()
    {
        I_RandomPlayer = Random.Range(0, GO_Players.Count);
        GO_Players[I_RandomPlayer].GetComponent<HotPotatoPlayer>().SetBomb();
        Debug.Log(I_RandomPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
        F_BombTimer -= Time.deltaTime;
        if (F_BombTimer <= 0)
        {
            for (int i = 0; i < GO_Players.Count; i++)
            {
                if (GO_Players[i].GetComponent<HotPotatoPlayer>().GetBomb())
                {
                    Destroy(GO_Players[i]);
                    GO_Players.Remove(GO_Players[i]);
                }
            }
            Debug.Log("timer expired");
            I_PlayersLeft--;
            F_BombTimer = 10;
            I_RandomPlayer = Random.Range(0, GO_Players.Count);
            GO_Players[I_RandomPlayer].GetComponent<HotPotatoPlayer>().SetBomb();
            Debug.Log(I_RandomPlayer);
        }

        for (int i = 0; i < GO_Players.Count; i++)
        {
            if (GO_Players[i].GetComponent<HotPotatoPlayer>().GetBomb())
            {
                GO_Bombprefab.transform.position = GO_Players[i].transform.position + V3_Offset;
                break;
            }
        }

        if (I_PlayersLeft == 1) 
        { 
            //end minigame
        }

    }
}
