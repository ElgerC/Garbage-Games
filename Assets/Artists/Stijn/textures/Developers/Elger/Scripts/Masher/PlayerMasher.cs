using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMasher : MonoBehaviour
{
    PlayerControls playerControls;

    private bool B_mashSpam = false;
    public float F_Presses = 0;
    public Vector3 V3_mashPos;
    public  GameObject G_mashNumber;
    public GameObject G_Canvas;
    public GameObject G_throw;

    public GameObject G_Pile;
    [SerializeField] private float F_pileShrink;

    public bool B_canAct = false;
    public void Mash_performed1()
    {
        Debug.Log("Mash1");
        if (B_canAct && B_mashSpam)
        {
            B_mashSpam = !B_mashSpam;
            F_Presses++;

            GameObject go = Instantiate(G_mashNumber, G_Canvas.transform);
            go.GetComponent<RectTransform>().anchoredPosition = V3_mashPos;
            go.GetComponent<MasherNumber>().S_text = F_Presses.ToString();

            Instantiate(G_throw, transform.position, Quaternion.identity);

            G_Pile.transform.localScale = new Vector3(G_Pile.transform.localScale.x - F_pileShrink, G_Pile.transform.localScale.y - F_pileShrink, G_Pile.transform.localScale.z - F_pileShrink);
        }
    }
    public void Mash_performed2()
    {
        if (B_canAct && !B_mashSpam)
        {
            B_mashSpam = !B_mashSpam;
            F_Presses++;

            GameObject go = Instantiate(G_mashNumber, G_Canvas.transform);
            go.GetComponent<RectTransform>().anchoredPosition = V3_mashPos;
            go.GetComponent<MasherNumber>().S_text = F_Presses.ToString();

            Instantiate(G_throw, transform.position, Quaternion.identity);

            G_Pile.transform.localScale = new Vector3(G_Pile.transform.localScale.x - F_pileShrink, G_Pile.transform.localScale.y - F_pileShrink, G_Pile.transform.localScale.z - F_pileShrink);
        }
    }
}
