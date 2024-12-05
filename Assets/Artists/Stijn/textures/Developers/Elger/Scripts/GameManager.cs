using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public List<GameObject> players = new List<GameObject>();
    private List<GameObject> lostPlayers = new List<GameObject>();

    public List<string> minigames = new List<string>();
    public List<string> L_playedMinigames = new List<string>();



    public string S_curMinigame = "StartScene";

    [SerializeField] private TMP_Text Txt_timerTxt;

    private bool B_countingDown = false;

    private int minigameIndex = 0;

    [SerializeField] private GamaManagerScrptObj gameManagerData;

    [SerializeField] private List<PlayerApearanceScrptObj> L_apearances = new List<PlayerApearanceScrptObj>();
    [SerializeField] private List<Vector3> V3_nameCardPositions = new List<Vector3>();
    [SerializeField] private GameObject G_nameCardPrefab;

    [SerializeField] private GameObject G_nameCardCanvas;

    private bool B_tieBreaker = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(G_nameCardCanvas);
    }
    public GameObject CreateNamecard()
    {

        GameObject go = Instantiate(G_nameCardPrefab, G_nameCardCanvas.transform);
        go.GetComponent<RectTransform>().anchoredPosition = V3_nameCardPositions[players.Count - 1];
        return go;
    }
    public void AddPlayers(GameObject newPlayer)
    {
        Debug.Log("Adding player");

        players.Add(newPlayer);

        newPlayer.GetComponent<PlayerScript>().ChangeApearance(L_apearances[players.Count - 1]);

        newPlayer.transform.position = gameManagerData.m_MinigamesData[minigameIndex].startPositions[players.Count - 1];
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (S_curMinigame == "StartScene")
        {
            Txt_timerTxt = GameObject.FindWithTag("Countdown").GetComponent<TMP_Text>();
            for (int i = 0; i < players.Count; i++)
            {
                players[i].GetComponent<PlayerScript>().ShowWins();
            }
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                PlayerScript PS_curScript = players[i].GetComponent<PlayerScript>();

                for (int j = 0; j < PS_curScript.L_crownList.Count; j++)
                {
                    Destroy(PS_curScript.L_crownList[j]);
                }
                PS_curScript.L_crownList = new List<GameObject>();
            }
        }
        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetActive(true);
            players[i].transform.position = gameManagerData.m_MinigamesData[minigameIndex].startPositions[i];
        }
    }
    public string ChooseScene()
    {
        S_curMinigame = null;
        while (S_curMinigame == null)
        {
            minigameIndex = Random.Range(1, minigames.Count-1);
            if (!L_playedMinigames.Contains(minigames[minigameIndex]))
            {
                S_curMinigame = minigames[minigameIndex];
            }
        }
        return S_curMinigame;
    }
    public void MinigameFinished(int winner)
    {
        if (!B_tieBreaker)
        {
            if (winner < players.Count)
            {
                players[winner].GetComponent<PlayerScript>().wins++;
            }

            L_playedMinigames.Add(SceneManager.GetActiveScene().name);

            minigameIndex = 0;
            S_curMinigame = "StartScene";
            SceneManager.LoadScene("StartScene");

            if (L_playedMinigames.Count < minigames.Count - 2)
            {
                StartCoroutine(Countdown(6));
            }
            else
            {
                CheckWinner();
            }
        }
        else
        {
            EndGame(players[winner]);
        } 
    }
    private void EndGame(GameObject G_winner)
    {
        players.Clear();
        players.Add(G_winner);

        for (int i = 0; i < lostPlayers.Count; i++)
        {
            players.Add(lostPlayers[i]);
        }
        SceneManager.LoadScene(minigames[minigames.Count - 1]);
    }
    private void CheckWinner()
    {
        int I_minWins = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<PlayerScript>().wins >= I_minWins)
            {
                I_minWins = players[i].GetComponent<PlayerScript>().wins;
            }
        }
        for (int i = 0;i < players.Count;i++)
        {
            if (players[i].GetComponent<PlayerScript>().wins < I_minWins)
            {
                lostPlayers.Add(players[i]);
                players.RemoveAt(i);            
            }
        }
        if(players.Count == 1)
        {
            EndGame(players[0]);
        }
        else
        {
            L_playedMinigames.RemoveAt(Random.Range(0, L_playedMinigames.Count));
            B_tieBreaker = true;
            StartCoroutine(Countdown(6));
        }
    }

    public void Ready()
    {
        if (L_playedMinigames.Count < minigames.Count - 2)
        {
            if (!B_countingDown)
            {
                Txt_timerTxt.gameObject.SetActive(true);
                StartCoroutine(Countdown(6));
            }

            else
            {
                Txt_timerTxt.gameObject.SetActive(false);
                StopAllCoroutines();
                B_countingDown = false;
                Txt_timerTxt.text = "0";
            }
        }
    }
    IEnumerator Countdown(int time)
    {
        B_countingDown = true;
        for (int i = 0; i < time; i++)
        {
            Txt_timerTxt.text = i.ToString();
            yield return new WaitForSeconds(time / time);
        }
        SceneManager.LoadScene(ChooseScene());
    }
}
