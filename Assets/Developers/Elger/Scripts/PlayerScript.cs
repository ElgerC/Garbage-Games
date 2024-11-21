using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gamemanager = Gamemanager.instance;
        DontDestroyOnLoad(gameObject);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && canAct)
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
        if (ctx.performed && gamemanager.S_curMinigame == "ElgerScene")
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


    void Update()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y).normalized * playerSpeed;
        rb.MovePosition(transform.position + move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        if (gamemanager.S_curMinigame == "HotPotato")
            if (rb.velocity.magnitude < 1 && rb.velocity.magnitude > -1)
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
    }
}