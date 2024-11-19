using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField] private Vector3 startPos;

    public List<GameObject> players = new List<GameObject>();
    public List<Scene> minigames = new List<Scene>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
    private void Start()
    {
        transform.position = startPos;
    }
    public void AddPlayer(GameObject obj)
    {
        players.Add(obj);

        obj.transform.position = transform.position;

        transform.position = new Vector3(transform.position.x + 5f, startPos.y, startPos.z);
    }
}
