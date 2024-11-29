using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HotPotatoPlayer : MonoBehaviour
{
    public HotPotato HotPotato;

    public int I_PlayerIndex;
    [SerializeField]
    private bool B_HasBomb = false;
    // Start is called before the first frame update
    void Start()
    {
        HotPotato = FindObjectOfType<HotPotato>();
        I_PlayerIndex = HotPotato.GO_Players.IndexOf(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        TransferPotato(collision.gameObject);
        Debug.Log(transform.tag);
    }
    public void TransferPotato(GameObject obj)
    {
        if (B_HasBomb && obj.transform.tag == "player1" || B_HasBomb &&  obj.transform.tag == "player4" || B_HasBomb && obj.transform.tag == "player3" || B_HasBomb &&  obj.transform.tag == "player2")
        {
            StartCoroutine(BombPassDown(obj));
            Debug.Log("switched");
        }
    }

    public void SetBomb()
    {
        B_HasBomb = true;
    }

    public bool GetBomb()
    {
        return B_HasBomb;
    }

    IEnumerator BombPassDown(GameObject target)
    {
        yield return new WaitForSeconds(0.2f);
        target.GetComponent<HotPotatoPlayer>().SetBomb();
        B_HasBomb = false;
        Debug.Log(HotPotato.I_RandomPlayer);
        HotPotato.I_RandomPlayer = I_PlayerIndex;
    }
}