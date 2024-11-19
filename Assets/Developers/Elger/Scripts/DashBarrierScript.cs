using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBarrierScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Vector3 movement = new Vector3(transform.forward.x, 0.1f, transform.forward.z);
            other.gameObject.GetComponent<Rigidbody>().AddForce(movement * 30, ForceMode.Impulse);

            Debug.Log(other);
            transform.parent.GetComponent<HotPotatoPlayer>().TransferPotato(other.gameObject);
        }
    }

}
