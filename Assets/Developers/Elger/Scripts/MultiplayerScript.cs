using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerScript : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    private PlayerInputManager playerInputManager;
    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        transform.position = startPos;
    }
    public void Move()
    {
        transform.position = new Vector3(transform.position.x + 5f, startPos.y, startPos.z);
    }
}
