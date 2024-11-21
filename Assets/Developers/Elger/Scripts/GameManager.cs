using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private ScriptableObject gameManagerData;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }

    public void AddPlayers()
    {
        Debug.Log("Adding players");

        players.Clear();
        PlayerScript[] playerScripts = FindObjectsOfType<PlayerScript>();
        for (int i = 0; i < playerScripts.Length; i++)
        {
            players.Add(playerScripts[i].gameObject);
        }
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
        for (int i = 0; i < players.Count; i++)
        {
            players[i].transform.position = 
        }
    }
    private string ChooseScene()
    {
        if (SceneManager.GetActiveScene().name != "StartScene")
        {
            minigames.Remove(SceneManager.GetActiveScene().name);
        }

        minigameIndex = UnityEngine.Random.Range(1, minigames.Count);
        S_curMinigame = minigames[minigameIndex];
        return S_curMinigame;
    }
    public void MinigameFinished()
    {
        SceneManager.LoadScene(ChooseScene());
    }

    public void Ready()
    {
        Debug.Log(B_countingDown);

        if (!B_countingDown)
            StartCoroutine(Countdown(5));
        else
        {
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
            if (Txt_timerTxt)
                Txt_timerTxt.text = i.ToString();
            yield return new WaitForSeconds(time / time);
        }
        MinigameFinished();
        B_countingDown = false;
    }
}
