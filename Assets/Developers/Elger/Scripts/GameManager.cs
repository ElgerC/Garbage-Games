using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField] private Vector3 startPos;

    public List<GameObject> players = new List<GameObject>();
    public List<string> minigames = new List<string>();
    public List<string> playedMinigames = new List<string>();

    [SerializeField] private Vector3[][] spawnPositions;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        SceneManager.LoadScene(ChooseScene());
        transform.position = startPos;
    }
    public void AddPlayer(GameObject obj)
    {
        players.Add(obj);

        obj.transform.position = transform.position;

        transform.position = new Vector3(transform.position.x + 5f, startPos.y, startPos.z);
    }
    private string ChooseScene()
    {
        string scene = null;
        while(scene == null)
        {
            string posibleScene = minigames[Random.Range(0, minigames.Count)];
            if(!playedMinigames.Contains(posibleScene))
                scene = posibleScene;
        }
        return scene;   
    }
    public void MinigameFinished()
    {
        playedMinigames.Add(SceneManager.GetActiveScene().name);
        ChooseScene();
    }
}
