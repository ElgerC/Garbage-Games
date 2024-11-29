using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    private float F_BombTimer = 10f;

    private Vector3 V3_Offset = new Vector3(0, 2, 0);

    [SerializeField]
    private TextMeshProUGUI T_BombTimer;

    Gamemanager gamemanager;

    private void Awake()
    {
        gamemanager =  Gamemanager.instance;
    }
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < FindObjectsOfType<PlayerScript>().Length; i++)
        {
            GO_Players.Add(FindObjectsOfType<PlayerScript>()[i].gameObject);
        }
        for (int i = 0; i < GO_Players.Count; i++)
        {
            GO_Players[i].AddComponent<HotPotatoPlayer>();
        }


        I_RandomPlayer = Random.Range(0, GO_Players.Count);
        GO_Players[I_RandomPlayer].GetComponent<HotPotatoPlayer>().SetBomb();
        Debug.Log(I_RandomPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        T_BombTimer.text = F_BombTimer.ToString();
        if (GO_Players.Count > 1)
        {
            F_BombTimer -= Time.deltaTime;
        }
        else
        {
            Destroy(GO_Bombprefab);
        }

        if (F_BombTimer <= 0)
        {
            for (int i = 0; i < GO_Players.Count; i++)
            {
                if (GO_Players[i].GetComponent<HotPotatoPlayer>().GetBomb())
                {
                    GO_Players[i].SetActive(false);
                    GO_Players.Remove(GO_Players[i]);
                }
            }
            Debug.Log("timer expired");
            I_PlayersLeft--;
            F_BombTimer = 15;
            I_RandomPlayer = Random.Range(0, GO_Players.Count);
            GO_Players[I_RandomPlayer].GetComponent<HotPotatoPlayer>().SetBomb();
            Debug.Log(I_RandomPlayer);
        }

        for (int i = 0; i < GO_Players.Count; i++)
        {
            if (GO_Players[i].GetComponent<HotPotatoPlayer>().GetBomb())
            {
                GO_Bombprefab.transform.position = GO_Players[i].transform.position + V3_Offset;
            }
        }

        if (GO_Players.Count == 1)
        {
            Gamemanager.instance.MinigameFinished(8);

            GO_Players[0].GetComponent<PlayerScript>().wins++;
        }
    }
}
