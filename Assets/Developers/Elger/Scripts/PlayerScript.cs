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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.mass += 1;
            movementInput = context.ReadValue<Vector2>();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && canAct)
        {
            switch (state)
            {
                case states.minigame1:
                    rb.mass = 1;

                    Vector3 movement = new Vector3(transform.forward.x, 0.1f, transform.forward.z);
                    rb.AddForce(movement * dashSpeed, ForceMode.Impulse);

                    barrier.SetActive(true);

                    StartCoroutine(ActionCD(4));
                    break;
                case states.minigame2:
                    Debug.Log(state);
                    break;
                case states.minigame3:
                    Debug.Log(state);
                    break;
                case states.minigame4:
                    Debug.Log(state);
                    break;
                case states.minigame5:
                    Debug.Log(state);
                    break;
            }
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

        if (rb.velocity.z < 1 && rb.velocity.z > -1)
        {
            barrier.SetActive(false);
        }
        else
        {
            barrier.SetActive(true);
        }

    }
}