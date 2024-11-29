using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    public List<GameObject> players = new List<GameObject>();
    public List<string> minigames = new List<string>();



    public string S_curMinigame = "ElgerScene";

    [SerializeField] private TMP_Text Txt_timerTxt;

    private bool B_countingDown = false;

    private int minigameIndex = 0;

    [SerializeField] private GamaManagerScrptObj gameManagerData;

    public List<Color> colorList = new List<Color>();
    public List<GameObject> L_models = new List<GameObject>();

    [SerializeField] private List<PlayerApearanceScrptObj> L_apearances = new List<PlayerApearanceScrptObj>();
    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Txt_timerTxt.transform.parent);
    }
    public void ChangeApearance(GameObject player)
    {
        int apearanceIndex = UnityEngine.Random.Range(0, L_apearances.Count - 1);
        Instantiate(L_apearances[apearanceIndex].G_model, new Vector3(0, -0.5f, 0), Quaternion.Euler(0, 90, 0), player.transform);

        PlayerScript PS_playerScript = player.GetComponent<PlayerScript>();

        PS_playerScript.C_playerColor = L_apearances[apearanceIndex].C_color;
        PS_playerScript.G_namecard = L_apearances[apearanceIndex].G_namecard;
    }
    public void AddPlayers(GameObject newPlayer)
    {
        Debug.Log("Adding player");

        players.Add(newPlayer);






        int modelIndex = UnityEngine.Random.Range(0, L_models.Count - 1);
        Instantiate(L_models[modelIndex], new Vector3(0, -0.5f, 0), Quaternion.Euler(0, 90, 0), newPlayer.transform);
        L_models.RemoveAt(modelIndex);

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
        Txt_timerTxt.gameObject.SetActive(false);

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
            //players.FindInstanceID<GameObject>(gameObject);
            players[winner].GetComponent<PlayerScript>().wins++;
        }

        minigames.Remove(SceneManager.GetActiveScene().name);

        minigameIndex = 0;
        S_curMinigame = "ElgerScene";
        SceneManager.LoadScene("ElgerScene");
        
        if (minigames.Count > 1) 
        {
            StartCoroutine(Countdown(5));
        } else
        {
            Txt_timerTxt.gameObject.SetActive(true);
            Txt_timerTxt.text = "Winner";
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
        Txt_timerTxt.gameObject.SetActive (true);
        B_countingDown = true;
        for (int i = 0; i < time; i++)
        {
                Txt_timerTxt.text = i.ToString();
            yield return new WaitForSeconds(time / time);
        }
        SceneManager.LoadScene(ChooseScene());
        Txt_timerTxt.gameObject.SetActive(false);
        B_countingDown = false;
    }
}
