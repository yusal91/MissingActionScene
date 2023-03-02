using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TrampolinJump : MonoBehaviour
{
    public float jumpForceMushrrom = 10f;

    private Rigidbody rb;


    private void Start()
    {
       rb= GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.playerMovement.onGroundThreshold = 0;

            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForceMushrrom, ForceMode.Impulse);
        }
    }

   

    private void OnCollisionExit(Collision collision)
    {
        GameManager.instance.playerMovement.onGroundThreshold = 1;
    }
}
