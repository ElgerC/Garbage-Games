using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float gravityValue = -9.81f;
    private Vector3 playerVelocity;
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerKnockDownHealth;

    private CharacterController characterController;

    private Vector2 movementInput = Vector2.zero;
    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            movementInput = context.ReadValue<Vector2>();
        }

    }

    void Update()
    {
        Vector3 move = new Vector3(movementInput.x, 0, movementInput.y);
        characterController.Move(move * Time.deltaTime * playerSpeed);
        /*        characterController.Move(playerVelocity * Time.deltaTime);*/
    }
}