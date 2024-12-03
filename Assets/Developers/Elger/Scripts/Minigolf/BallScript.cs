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

    public bool B_landed = false;
    public bool B_Lauched = false;

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
        MinigolfManager.instance.G_club.SetActive(true);
        MinigolfManager.instance.Anim_clubAnim.SetTrigger("");
    }
    public void Launch()
    {
        if (!B_Lauched)
        {
            Debug.Log("Launch");
            V3_direction = -G_Pivot.transform.forward * F_speed * (G_Pivot.transform.GetChild(0).localScale.z * 3);


            Anim_animator.enabled = false;
            G_Pivot.SetActive(false);

            RB_rb.AddForce(V3_direction);
            B_Lauched = true;
        }
    }
    private void Update()
    {
        if (RB_rb.velocity.magnitude < MinSpeed.magnitude && B_Lauched && RB_rb.velocity.magnitude > 0f)
        {
            RB_rb.velocity = Vector3.zero;
        }
        if (RB_rb.velocity == Vector3.zero && B_Lauched)
        {
            StartCoroutine(minTime());
        }

        if (B_SavedRot)
        {
            transform.rotation = Q_SavedRotation;
        }

        if (B_Lauched)
        {
            float xRot = transform.rotation.x + RB_rb.velocity.x * 60;
            float yRot = transform.rotation.y + RB_rb.velocity.y * 60;
            float zRot = transform.rotation.z + RB_rb.velocity.z * 60;

            transform.rotation = Quaternion.Euler(xRot, yRot, zRot);
        }
    }
    private IEnumerator minTime()
    {
        yield return new WaitForSeconds(2);
        if (RB_rb.velocity == Vector3.zero && B_Lauched)
        {
            B_landed = true;
        }
    }
}
