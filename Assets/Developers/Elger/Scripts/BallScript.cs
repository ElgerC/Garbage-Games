using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private Rigidbody RB_rb;
    private Animator Anim_animator;

    [SerializeField] private Vector3 V3_direction;
    [SerializeField] private float F_speed;

    private Vector3 MinSpeed = new Vector3(0.5f, 0.5f, 0.5f);

    [SerializeField] private Quaternion Q_SavedRotation;
    private bool B_SavedRot = false;

    private GameObject G_Pivot;
    private void Awake()
    {
        RB_rb = GetComponent<Rigidbody>();
        Anim_animator = GetComponent<Animator>();
        G_Pivot = transform.GetChild(0).gameObject;
    }
    public void LockRotation()
    {
        Q_SavedRotation = G_Pivot.transform.rotation;
        B_SavedRot = true;

        Anim_animator.SetTrigger("LockRotate");
    }
    public void Launch()
    {
        V3_direction = transform.forward * F_speed * G_Pivot.transform.GetChild(0).localScale.z;
        Debug.Log(RB_rb.velocity);

        Anim_animator.enabled = false;
        G_Pivot.SetActive(false);

        
        RB_rb.AddForce(V3_direction);
    }
    private void Update()
    {
        if(RB_rb.velocity.magnitude < MinSpeed.magnitude)
        {
            RB_rb.velocity = Vector3.zero;
        }
        if(Input.GetMouseButtonDown(0))
        {
            LockRotation();
        } else if (Input.GetMouseButtonUp(0))
        {
            Launch();
        }
        if(B_SavedRot)
        {
            transform.rotation = Q_SavedRotation;
        }
    }
}
