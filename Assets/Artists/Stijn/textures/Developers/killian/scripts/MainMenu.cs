using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("ElgerScene");


        //Edited by Elger
        if(FindObjectOfType<Gamemanager>() != null)
        {
            for (int i = 0; i < Gamemanager.instance.players.Count; i++)
            {
                Destroy(FindObjectOfType<PlayerScript>().gameObject);
            }
            Destroy(FindObjectOfType<Gamemanager>().G_nameCardCanvas);
            Destroy(FindObjectOfType<Gamemanager>().gameObject);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
