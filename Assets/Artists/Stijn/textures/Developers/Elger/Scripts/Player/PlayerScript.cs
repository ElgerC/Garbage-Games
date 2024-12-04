using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum states
{
    minigame1,
    minigame2,
    minigame3,
    minigame4,
    minigame5,
}
public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float playerSpeed;

    private Rigidbody rb;

    private Vector2 movementInput = Vector2.zero;

    [SerializeField] states state;

    [SerializeField] private bool canAct = true;

    [SerializeField] private float dashSpeed;

    [SerializeField] private GameObject barrier;

    Gamemanager gamemanager;

    [SerializeField] private GameObject G_stepLower;
    [SerializeField] private GameObject G_stepUpper;

    [SerializeField] private float F_stepSmooth = 8f;

    public GameObject G_golfBall;

    public Color C_playerColor;
    public Sprite S_namecard;

    private GameObject G_namecard;

    public int wins = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gamemanager = Gamemanager.instance;
        DontDestroyOnLoad(gameObject);

        transform.tag = "player" + (gamemanager.players.Count + 1);
    }
    private void Start()
    {
        gamemanager.AddPlayers(gameObject);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed && gamemanager.S_curMinigame == "HotPotato" || context.performed && gamemanager.S_curMinigame == "RandomDoor")
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if(G_golfBall != null && gamemanager.S_curMinigame == "MiniGolf")
        {
            if (context.started)
            {
                G_golfBall.GetComponent<BallScript>().LockRotation();
            }

            else if (context.canceled)
            {
                G_golfBall.GetComponent<BallScript>().Launch();
            }
        }
        

        else if (context.performed && canAct)
        {
            Debug.Log(gamemanager.S_curMinigame);
            switch (gamemanager.S_curMinigame)
            {
                case "HotPotato":
                    rb.drag = 0;

                    Vector3 movement = new Vector3(transform.forward.x, 0.1f, transform.forward.z);
                    rb.AddForce(movement * dashSpeed, ForceMode.Impulse);

                    barrier.SetActive(true);

                    StartCoroutine(ActionCD(4));
                    break;
                default: break;
            }
        }

    }

    public void OnReady(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && gamemanager.S_curMinigame == "StartScene")
        {
            gamemanager.Ready();
        }
    }
    private IEnumerator ActionCD(float cooldown)
    {
        canAct = false;
        yield return new WaitForSeconds(cooldown);
        canAct = true;
    }

    private void Update()
    {
        StepClimb();

        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y).normalized * playerSpeed;
        rb.MovePosition(rb.position + move);

        if(gamemanager.S_curMinigame == "StartScene")
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (move != Vector3.zero)
        {
            rb.velocity = move;
            transform.forward = move;
        }
        if (gamemanager.S_curMinigame == "HotPotato" || gamemanager.S_curMinigame == "RandomDoor")
            if (rb.velocity.magnitude < 4f && rb.velocity.magnitude > -1f)
            {
                rb.drag = 0;
                barrier.SetActive(false);
            }
            else
            {
                rb.drag += 0.2f;
                barrier.SetActive(true);
            }
        else
            barrier.SetActive(false);
        if(gamemanager.S_curMinigame == "MiniGolf")
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } 

    }

    private void StepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(G_stepLower.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.1f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(G_stepUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.1f))
            {
                rb.position += new Vector3(0f, F_stepSmooth, 0f);
            }
        }
        RaycastHit hitLowerPlus45;
        if (Physics.Raycast(G_stepLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitLowerPlus45, 0.1f))
        {
            RaycastHit hitUpperPlus45;
            if (!Physics.Raycast(G_stepUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out hitUpperPlus45, 0.1f))
            {
                rb.position += new Vector3(0f, F_stepSmooth, 0f);
            }
        }
        RaycastHit hitLowerMin45;
        if (Physics.Raycast(G_stepLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitLowerMin45, 0.1f))
        {
            RaycastHit hitUpperMin45;
            if (!Physics.Raycast(G_stepUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out hitUpperMin45, 0.1f))
            {
                rb.position += new Vector3(0f, F_stepSmooth, 0f);
            }
        }
    }

    public void ChangeApearance(PlayerApearanceScrptObj apearance)
    {
        Instantiate(apearance.G_model, new Vector3(0, -0.5f, 0), Quaternion.Euler(0, 90, 0), transform);
        C_playerColor = apearance.C_color;

        if (!G_namecard)
        {
            G_namecard = gamemanager.CreateNamecard();
        }
        G_namecard.GetComponent<Image>().sprite = apearance.namecard;
    }
    public void ShowWins()
    {
        G_namecard.GetComponent<NameCardScript>().PlaceCrowns(wins);
    }
}