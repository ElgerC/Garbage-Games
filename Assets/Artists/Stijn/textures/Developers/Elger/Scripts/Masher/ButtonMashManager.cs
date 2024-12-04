using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonMashManager : MonoBehaviour
{
    [SerializeField] private List<PlayerMasher> L_PlayerScripts = new List<PlayerMasher>();
    Gamemanager gamemanager;
    [SerializeField] float winPresses;

    [SerializeField] private List<Vector3> L_NumberPos = new List<Vector3>();
    [SerializeField] private GameObject G_mashNumber;

    [SerializeField] private GameObject G_throw;

    [SerializeField] private List<GameObject> G_piles = new List<GameObject>();

    [SerializeField] private TMP_Text Txt_countDown;

    private Animator Anim_cameraAnimator;
    [SerializeField] private GameObject G_crown;
    private void Awake()
    {
        gamemanager = Gamemanager.instance;

        Anim_cameraAnimator = GetComponent<Animator>();
        for (int i = 0; i < gamemanager.players.Count; i++)
        {
            PlayerMasher go = Gamemanager.instance.players[i].AddComponent<PlayerMasher>();

            go.V3_mashPos = L_NumberPos[i];
            go.G_mashNumber = G_mashNumber;
            go.G_throw = G_throw;
            go.G_Pile = G_piles[i];
            
            L_PlayerScripts.Add(go);
        }
    }
    private void Update()
    {
        for(int i = 0;i < L_PlayerScripts.Count;i++)
        {
            if (L_PlayerScripts[i].F_Presses >= winPresses)
            {
                Anim_cameraAnimator.SetTrigger("Outro");
                CanPressSwitch(false);

                StartCoroutine(OutroCountdown(i));
            }
        }
    }
    private IEnumerator OutroCountdown(int I_winnerIndex)
    {
        Instantiate(G_crown, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(8);
        gamemanager.MinigameFinished(I_winnerIndex);
    }
    public void StartGame()
    {
        StartCoroutine(Countdown());
    }
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1);
        Txt_countDown.gameObject.SetActive(true);
        Txt_countDown.text = "Ready?";
        yield return new WaitForSeconds(1);
        Txt_countDown.text = "Set";
        yield return new WaitForSeconds(1);
        Txt_countDown.text = "Go!";
        yield return new WaitForSeconds(0.5f);
        Txt_countDown.gameObject.SetActive(false);
        CanPressSwitch(true);
    }
    private void CanPressSwitch(bool option)
    {
        for (int i = 0; i < L_PlayerScripts.Count; i++)
        {
            L_PlayerScripts[i].B_canAct = option;
        }
    }
}
