using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    CharacterController playerController;
    private Vector2 movementInput = Vector2.zero;
    public float speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<CharacterController>();
    }
    public void Movement(InputAction.CallbackContext ctx)
    {
        Debug.Log("move");
        movementInput = ctx.ReadValue<Vector2>();
    }
    public void Interact(InputAction.CallbackContext ctx) 
    {
        if (ctx.performed)
        {
            Debug.Log("test");
        }
    }
    //private void Update()
    //{
    //    Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
    //    playerController.Move(move * Time.deltaTime * speed);
    //}
}
