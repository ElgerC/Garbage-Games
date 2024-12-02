using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public List<GameObject> players = new List<GameObject>();
    public List<string> minigames = new List<string>();



    public string S_curMinigame = "StartScene";

    [SerializeField] private TMP_Text Txt_timerTxt;

    private bool B_countingDown = false;

    private int minigameIndex = 0;

    [SerializeField] private GamaManagerScrptObj gameManagerData;

    public List<Color> colorList = new List<Color>();
    public List<GameObject> L_models = new List<GameObject>();

    [SerializeField] private List<PlayerApearanceScrptObj> L_apearances = new List<PlayerApearanceScrptObj>();
    [SerializeField] private List<Vector3> V3_nameCardPositions = new List<Vector3>();
    [SerializeField] private GameObject G_nameCardPrefab;

    [SerializeField] private GameObject G_nameCardCanvas;
    private RectTransform CanvasRect;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);

        CanvasRect = G_nameCardCanvas.GetComponent<RectTransform>();
    }
    public GameObject CreateNamecard()
    {
        int apearanceIndex = UnityEngine.Random.Range(0, L_apearances.Count - 1);

        GameObject go = Instantiate(G_nameCardPrefab,G_nameCardCanvas.transform);
        go.GetComponent<RectTransform>().anchoredPosition = V3_nameCardPositions[players.Count - 1];
        return go;
    }
    public void AddPlayers(GameObject newPlayer)
    {
        Debug.Log("Adding player");

        players.Add(newPlayer);

        newPlayer.GetComponent<PlayerScript>().ChangeApearance(L_apearances[players.Count-1]);

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
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetActive(true);
            players[i].transform.position = gameManagerData.m_MinigamesData[minigameIndex].startPositions[i];
        }
    }
    public string ChooseScene()
    {
        minigameIndex = UnityEngine.Random.Range(1, minigames.Count);
        S_curMinigame = minigames[minigameIndex];
        return S_curMinigame;
    }
    public void MinigameFinished(int winner)
    {
        if (winner < players.Count)
        {
            players[winner].GetComponent<PlayerScript>().wins++;
        }

        minigames.Remove(SceneManager.GetActiveScene().name);

        minigameIndex = 0;
        S_curMinigame = "StartScene";
        SceneManager.LoadScene("StartScene");
        
        if (minigames.Count > 1) 
        {
            StartCoroutine(Countdown(6));
        } else
        {
            Application.Quit();
        }
    }

    public void Ready()
    {
        Debug.Log(B_countingDown);

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
