using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody RB_rb;
    [SerializeField] private Vector3 V3_direction;
    [SerializeField] private float F_speed;
    Vector3 velo;
    private Vector3 MinSpeed = new Vector3(0.5f, 0.5f, 0.5f);
    private void Awake()
    {
        RB_rb = GetComponent<Rigidbody>();
        V3_direction = transform.forward * F_speed;
    }
    private void Start()
    {
        RB_rb.AddForce(V3_direction);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(velo);
        RB_rb.AddForce(Vector3.Reflect(V3_direction, collision.contacts[0].normal));
        V3_direction = Vector3.Reflect(velo, collision.contacts[0].normal);
    }
    private void Update()
    {
        if (RB_rb.velocity.magnitude > MinSpeed.magnitude)
            velo = RB_rb.velocity;
    }
}
